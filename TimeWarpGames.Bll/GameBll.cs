using System;
using System.Collections.Generic;
using TimeWarpGames.Dal;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Bll
{
    public static class GameBll
    {
        public static List<Game> ReadAll()
        {
           List<Game> lstGames = GameDal.ReadAll();
           if (lstGames == null)
           {
               throw new Exception("Geen Games gevonden");
           }
           return lstGames;
        }
    }
}