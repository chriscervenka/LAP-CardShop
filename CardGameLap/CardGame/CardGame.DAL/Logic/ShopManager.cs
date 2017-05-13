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
    public class ShopManager
    {

        /// <summary>
        /// 
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
        /// 
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
        /// Methode ORDERPACK erstellt PACK mit zufällig erstellten Karten aus TBLCARD
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfPacks"></param>
        /// <returns>return generatedCards</returns>
        public static List<Card> OrderPack(int id, int numberOfPacks)
        {
            Random rnd = new Random();
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfPacks"></param>
        /// <returns></returns>
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
       
    }
}
