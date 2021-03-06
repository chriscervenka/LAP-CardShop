﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.DAL.Model;
using CardGame.Log;


namespace CardGame.DAL.Logic
{
    public class DBInfoManager
    {
        public static int GetNumUsers()
        {
            int numUsers = -1;
            using (var db = new ClonestoneFSEntities())
            {
                numUsers = db.AllPersons.Count();
            }

            Writer.LogInfo("GetNumUsers " + numUsers);

            return numUsers;
        }


        public static int GetNumCards()
        {
            int numCards = -1;
            using (var db = new ClonestoneFSEntities())
            {
                numCards = db.AllCards.Count();
            }

            Writer.LogInfo("GetNumCards " + numCards);

            return numCards;
        }


        public static int GetNumDecks()
        {
            int numDecks = -1;
            using (var db = new ClonestoneFSEntities())
            {
                numDecks = db.AllDecks.Count();
            }

            Writer.LogInfo("GetNumDecks " + numDecks);

            return numDecks;
        }



        /// <summary>
        /// LAP Methode für STATISTIK
        /// Methode um aus Datenbank alle PACKS zu ermitteln
        /// </summary>
        /// <returns>numPacks</returns>
        public static int GetNumPacks()
        {
            int numPacks = -1;
            using (var db = new ClonestoneFSEntities())
            {
                numPacks = db.AllPacks.Count();
            }

            return numPacks;
        }

        public static int GetMostBuyedPacks()
        {
            int mostBuyedPacks = -1;
            using (var db = new ClonestoneFSEntities())
            {
                mostBuyedPacks = db.AllOrders.Count();
            }
            return mostBuyedPacks;
        }
    }
}
