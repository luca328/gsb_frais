using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFrais.Model.business
{
    public class FicheFrais
    {
        private Visiteur _visiteur;
        private string _mois;
        private Etat _etat;
        private decimal _montantValide;
        private int _nbJustificatifs;
        private DateTime _datemodif;
        private List<LigneFraisForfait> _lesLignesFraisForfaits = new List<LigneFraisForfait>();
        private List<LigneFraisHorsForfait> _lesLignesFraisHorsForfaits = new List<LigneFraisHorsForfait>();

        public FicheFrais(string mois, int nbJustificatifs, decimal montantValide, DateTime uneDateModif, Visiteur unVisiteur, Etat unEtat)
        {
            this._mois = mois;
            this._nbJustificatifs = nbJustificatifs;
            this._montantValide = montantValide;
            this._datemodif = uneDateModif;
            this._visiteur = unVisiteur;
            this._etat = unEtat;
        }

        public override string ToString()
        {
            return "Fiche frais" + Mois + "-" + _visiteur.Id + "-" + _visiteur.Prenom + " " + _visiteur.Nom;
        }

        public List<LigneFraisForfait> LesLignesFraisForfait
        {
            get
            {
                return _lesLignesFraisForfaits;
            }
            set
            {
                _lesLignesFraisForfaits = value;
            }
        }

        public Visiteur UnVisiteur
        {
            get
            {
                return _visiteur;
            }

            set
            {
                _visiteur = value;
            }
        }

        public string Mois
        {
            get
            {
                return _mois;
            }

            set
            {
                _mois = value;
            }
        }

        public Etat UnEtat
        {
            get
            {
                return _etat;
            }

            set
            {
                _etat = value;
            }
        }

        public decimal MontantValide
        {
            get
            {
                return _montantValide;
            }

            set
            {
                _montantValide = value;
            }
        }

        public int NbJustificatifs
        {
            get
            {
                return _nbJustificatifs;
            }

            set
            {
                _nbJustificatifs = value;
            }
        }

        public DateTime Datemodif
        {
            get
            {
                return _datemodif;
            }

            set
            {
                _datemodif = value;
            }
        }

        public List<LigneFraisHorsForfait> LesLignesFraisHorsForfaits
        {
            get
            {
                return _lesLignesFraisHorsForfaits;
            }

            set
            {
                _lesLignesFraisHorsForfaits = value;
            }
        }
    }
}
