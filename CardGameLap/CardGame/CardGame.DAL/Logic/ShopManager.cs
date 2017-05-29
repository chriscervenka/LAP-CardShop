using CardGame.DAL.Model;
using CardGame.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.DAL.Logic
{
    public enum BuyResult
    {
        Success,
        NotEnoughMoney
    }


    public class ShopManager
    {

        /// <summary>
        /// LUHN Algorithmus
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int getLuhn(string data)
        {
            int sum = 0;
            bool odd = false;
            for (int i = data.Length - 1; i >= 0; i--)
            {
                if (!odd)
                {
                    int tSum = (data[i] - '0') * 2;
                    if (tSum >= 10)
                    {
                        tSum = tSum - 9;
                    }
                    sum += tSum;
                }
                else
                    sum += (data[i] - '0');
                odd = !odd;
            }
            int erg = (((sum / 10) + 1) * 10) - sum;
            if (erg % 10 == 0)
            {
                erg = 0;
            }
            return erg;
        }



        /// <summary>
        /// Alle PACK aus der Tabelle PACK ohne PARAMETER
        /// </summary>
        /// <returns>return allPacks</returns>
        public static List<Pack> AllCardPacks()
            
        {
            List<Pack> allPacks = null;
            //var allPacks = new List<Pack>();

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    allPacks = db.Packs.OrderBy(x => x.Cardquantity).ToList();
                }

                if (allPacks == null)
                {
                    throw new Exception("kein Pack gefunden");
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
            }           
            return allPacks;
        }


        /// <summary>
        /// Gibt mir aus Tabelle PACK die ID von jedem PACK
        /// </summary>
        /// <param name="id"></param>
        /// <returns> return dbCardPack</returns>
        public static Pack GetCardPackById(int id)
        {
            var dbCardPack = new Pack();

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    dbCardPack = db.Packs.Find(id);
                }
                if (dbCardPack == null)
                    throw new Exception("kein Pack gefunden");
            }
            catch (Exception e)
            {
                Writer.LogError(e);
            }
            return dbCardPack;
        }



        /// <summary>
        /// Methode ORDERPACK erstellt PACK mit zufällig erstellten Karten aus CARDS
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfPacks"></param>
        /// <returns>return generatedCards</returns>
        public static List<Card> OrderPack(int id, int numberOfPacks)
        {
            //Random Instanz
            Random rnd = new Random();

            //Liste von Card auf generatedCards gespeichert
            var generatedCards = new List<Card>();

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    var cardPack = db.Packs.Find(id);

                    if (cardPack == null)
                    {
                        throw new Exception("Pack not found");
                    }

                    int numCardsToGenerate = cardPack.Cardquantity ?? 0;

                    numCardsToGenerate *= numberOfPacks;

                    var validIDs = db.Cards.Select(c => c.ID).ToList();

                    Writer.LogInfo("ID: " + validIDs.Count.ToString());

                    if (validIDs.Count == 0)
                    {
                        throw new Exception("No Card found");
                    }

                    //zufällig generierte Cards für Pack
                    for (int i = 0; i < numCardsToGenerate; i++)
                    {
                        int indexId = rnd.Next(0, validIDs.Count - 1);
                        int generatedCardId = validIDs[indexId];
                        var generatedCard = db.Cards.Where(c => c.ID == generatedCardId).Include(c => c.Type).FirstOrDefault();

                        //Abfrage ob generatedCard NULL (nicht vorhanden) ist
                        //TODO  ODER ob generatedCard mehrfach vorkommt !!!!
                        if (generatedCard == null)
                        {
                            throw new Exception("Card not found");
                        }

                        generatedCards.Add(generatedCard);
                    }
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
            }

            foreach (var card in generatedCards)
            {
                Writer.LogInfo("Card: " + card.ID);
            }
            
            return generatedCards;
        }


        /// <summary>
        /// Methode berechnet die TOTALCOST 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfPacks"></param>
        /// <returns>den PREIS aus price * numberOfPacks</returns>
        public static int TotalCost(int id, int numberOfPacks)
        {
            int price = 0;

            using (var db = new ClonestoneFSEntities())
            {
                var pack = db.Packs.Find(id);
                if (pack == null)
                {
                    throw new Exception("No Pack found");
                }
                //Convert a nullable DECIMAL to INT
                price = (int)pack.Packprice;
            }

            return price * numberOfPacks;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        private static Random RandomNumberGenerator = new Random();

        public static BuyResult BuyPack(int id, string email)
        {
            BuyResult result = BuyResult.Success;

            if (id <= 0)
                throw new ArgumentException("Invalid Value", nameof(id));

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            try
            {
                using (var context = new ClonestoneFSEntities())
                {
                    Person user = context.Person.FirstOrDefault(x => x.Email == email);
                    if (user == null)
                        throw new ArgumentException("Invalid value", nameof(email));

                    Pack cardPack = context.Packs.FirstOrDefault(x => x.ID == id);
                    if (cardPack == null)
                        throw new ArgumentException("Invalid value", nameof(id));


                    if (cardPack.IsMoney == false)
                    {
                        if (user.Currencybalance < cardPack.Packprice)
                        {
                            result = BuyResult.NotEnoughMoney;
                        }
                        else
                        {
                            user.Currencybalance -= (int)cardPack.Packprice;

                            Order purchase = new Order()
                            {
                                ID_Pack = id,
                                ID_Person = user.ID,
                                NumberOfPacks = 1,
                                Orderdate = DateTime.Now
                                
                            };
                            context.Orders.Add(purchase);


                            int count = context.Cards.Count();

                            //List<Card> userCards = new List<Card>();

                            /// create cards at random
                            for (int numberOfCard = 0; numberOfCard < cardPack.Cardquantity; numberOfCard++)
                            {
                                /// get a valid idCard (generated by random)
                                Card randomCard = context.Cards.OrderBy(x => x.ID).Skip(RandomNumberGenerator.Next(0, count)).Take(1).Single();

                                /// save new card to userCards

                                /// if card is already an userCard
                                /// increase number
                                Collection userCard = user.AllCollections.Where(x => x.ID == randomCard.ID).FirstOrDefault();
                                if (userCard != null)
                                {
                                    userCard.NumberOfCards++;
                                }
                                else /// else - add new userCard
                                {
                                    userCard = new Collection()
                                    {
                                        ID_Person = user.ID,
                                        ID = randomCard.ID,
                                        ID_Order = purchase.ID,
                                        NumberOfCards = 1
                                    };
                                    context.Collections.Add(userCard);
                                }
                                context.SaveChanges();
                            }
                            //context.SaveChanges();
                            //context.Entry(context.Collections).State = EntityState.Modified;
                        }
                    }

                    else if (cardPack.IsMoney == true)
                    {
                        Order purchase = new Order()
                        {
                            ID_Pack = id,
                            ID_Person = user.ID,
                            NumberOfPacks = 1,
                            Orderdate = DateTime.Now
                        };
                        context.Orders.Add(purchase);


                        int currentBalance = UserManager.GetCurrencyBalanceByEmail(user.Email);

                        int sumBalance = ((int)(currentBalance + GetCardPackById(id).DiamondValue)) * (int)purchase.NumberOfPacks;

                        bool isUpdated = UserManager.BalanceUpdateByEmail(user.Email, sumBalance);

                        context.SaveChanges();
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
