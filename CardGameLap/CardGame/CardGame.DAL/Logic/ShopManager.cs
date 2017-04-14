using CardGame.DAL.Model;
using CardGame.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.DAL.Logic
{
    public class ShopManager
    {
        /// <summary>
        /// Methode GetCardsFromPack
        /// gibt mir die zufällig generierten CARDS vom gekauften PACK in Liste zurück
        /// </summary>
        /// <returns></returns>
       
        public static List<tblcard> GetCardsFromPack()
        {
            List<tblcard> ReturnList = null;

            using (var db = new ClonestoneFSEntities())
            {
                var pack = db.tblpack.Find();

                if (pack == null)
                {
                    throw new Exception("Pack not found");
                }
                int cardquantity = 5;

                int numCardsToGenerate = (cardquantity);

                int numPacks = 1;
                numCardsToGenerate *= numPacks;

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
                    var generatedCard = db.tblcard.Where(c => c.idcard == generatedCardId);

                    if (generatedCard == null)
                    {
                        throw new Exception("Card not found");
                    }

                }

            }

            return ReturnList;
        }
    }
}
