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
        public static List<tblpack> AllCardPacks()
        {
            var allPacks = new List<tblpack>();

            using (var db = new ClonestoneFSEntities())
            {
                allPacks = db.tblpack.ToList();
            }

            if (allPacks == null)
            {
                throw new Exception("kein Pack gefunden");
            }
            return allPacks;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> return dbCardPack</returns>
        public static tblpack GetCardPackById(int? id)
        {
            var dbCardPack = new tblpack();

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    dbCardPack = db.tblpack.Find(id);
                }
                if (dbCardPack == null)
                    throw new Exception("CardPackNotFound");
            }
            catch (Exception e)
            {
                Writer.LogError(e);
            }
            return dbCardPack;
        }



        /// <summary>
        /// Methode ORDERPACK erstellt PACK mit 5 zufällig erstellten Karten aus TBLCARD
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfPacks"></param>
        /// <returns>return generatedCards</returns>
        public static List<tblcard> OrderPack(int id, int numberOfPacks)
        {
            var generatedCards = new List<tblcard>();

            using (var db = new ClonestoneFSEntities())
            {
                var cardPack = db.tblpack.Find(id);

                if (cardPack == null)
                {
                    throw new Exception("Pack not found");
                }
                int cardquantity = 5;

                int numCardsToGenerate = (cardquantity);

        
                numCardsToGenerate *= numberOfPacks;

                var validIDs = db.tblcard.Select(c => c.idcard).ToList();

                Writer.LogInfo("ID: " + validIDs.Count.ToString());

                if (validIDs.Count == 0)
                {
                    throw new Exception("No Card found");
                }

                //zufällig generierte 5 Cards für Deck
                for (int i = 1; i < numCardsToGenerate; i++)
                {
                    Random rnd = new Random();
                    int indexId = rnd.Next(0, validIDs.Count - 1);
                    int generatedCardId = validIDs[indexId];
                    var generatedCard = db.tblcard.Where(c => c.idcard == generatedCardId).Include(c => c.tbltype).FirstOrDefault();

                    //Abfrage ob generatedCard NULL (nicht vorhanden) ist
                    //TODO  ODER ob generatedCard mehrfach vorkommt !!!!
                    if (generatedCard == null)
                    {
                        throw new Exception("Card not found");
                    }

                    generatedCards.Add(generatedCard);
                }
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
                var pack = db.tblpack.Find(id);
                if (pack == null)
                {
                    throw new Exception("No Pack found");
                }

                //Convert a nullable DECIMAL to INT
                price = (int)pack.packprice;
            }

            return price * numberOfPacks;
        }
       
    }
}
