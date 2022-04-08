using GSBFrais.Model.data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPFFrais
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Dbal myDbal;
        private DaoEtat myEtat;
        private DaoFicheFrais myFicheFrais;
        private DaoFraisForfait myFraisForfait;
        private DaoLigneFraisForfait myLigneFraisForfait;
        private DaoLigneFraisHorsForfait myLigneFraisHorsForfait;
        private DaoVisiteur myVisiteur;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            myDbal = new Dbal("gsb_frais");
            myEtat = new DaoEtat(myDbal);
            myVisiteur = new DaoVisiteur(myDbal);
            myFraisForfait = new DaoFraisForfait(myDbal);
            myLigneFraisForfait = new DaoLigneFraisForfait(myDbal,myFraisForfait);
            myLigneFraisHorsForfait = new DaoLigneFraisHorsForfait(myDbal);
            myFicheFrais = new DaoFicheFrais(myDbal, myVisiteur, myEtat, myLigneFraisForfait, myLigneFraisHorsForfait);


            AddFrais2000 wnd = new AddFrais2000(myFicheFrais, myEtat, myLigneFraisHorsForfait);
            wnd.Show();
        }
    }
}
