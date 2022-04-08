using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSBFrais.Model.data;
using GSBFrais.Model.business;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Dbal myDbal = new Dbal();

            //myDbal.CUDQuery("INSERT INTO visiteur Values ('a135','demartini','luca', 'ldemartini','fdjghp','8 rue de l aube', '83000', 'sanary', '2021-03-13')");
            //myDbal.delete("visiteur where id = 'a135'");
            //myDbal.update("visiteur set id = 'a133' where id = 'a135'");
            //DataTable datas = new DataTable();
            //datas = myDbal.SelectAll("source");
            //Console.WriteLine(datas.Rows[0].ItemArray[0].ToString() + datas.Rows[0].ItemArray[1].ToString());
            //Console.Read();
            DaoVisiteur monDaoVisiteur = new DaoVisiteur(myDbal);
            DaoEtat monDaoEtat = new DaoEtat(myDbal);
            DaoFraisForfait myDaoFraisForfait = new DaoFraisForfait(myDbal);
            DaoLigneFraisHorsForfait myDaoLigneFraisHorsForfait = new DaoLigneFraisHorsForfait(myDbal);
            DaoLigneFraisForfait myDaoligneFraisForfait = new DaoLigneFraisForfait(myDbal, myDaoFraisForfait);
            //Etat unEtat = new Etat("RB", "Remboursée");
            //Visiteur unVisiteur = new Visiteur("a135", "derepierre");
            //FicheFrais uneFichefrais = new FicheFrais("202105", 10, (decimal)3000.05, DateTime.Today, unVisiteur, unEtat);
            //monDeuxiemeDao.Insert(uneFichefrais);
            //Visiteur maLigne = monDao.SelectById("a135");
            //Console.WriteLine(maLigne.Id);
            //Console.Read();
            DaoFicheFrais unDaoFichefrais = new DaoFicheFrais(myDbal, monDaoVisiteur, monDaoEtat, myDaoligneFraisForfait, myDaoLigneFraisHorsForfait);
            //List<FicheFrais> listFiche = unDaoFichefrais.SelectByMonth("202108");
            List<string> listFiche = unDaoFichefrais.SelectListMois();

            Console.WriteLine(listFiche);
            Console.Read();
        }
    }
}
