using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSBFrais.Model.business;
using GSBFrais.Model.data;
using System.Data;

namespace GSBFrais.Model.data
{
    public class DaoFicheFrais
    {
        private Dbal _dbal;
        private DaoVisiteur _daoVisiteur;
        private DaoEtat _daoEtat;
        private DaoLigneFraisForfait _daoLigneFraisForfait;
        private DaoLigneFraisHorsForfait _daoLigneFraisHorsForfait;

        public DaoFicheFrais(Dbal mydbal, DaoVisiteur unDaoVisiteur, DaoEtat unDaoEtat, DaoLigneFraisForfait uneDaoLigneFraisForfait, DaoLigneFraisHorsForfait uneDaoLigneFraisHorsForfait)
        {
            this._dbal = mydbal;
            this._daoVisiteur = unDaoVisiteur;
            this._daoEtat = unDaoEtat;
            this._daoLigneFraisForfait = uneDaoLigneFraisForfait;
            this._daoLigneFraisHorsForfait = uneDaoLigneFraisHorsForfait;
        }

        public void Insert(FicheFrais uneFicheFrais)
        {
            string query = " fichefrais(idVisiteur, mois, nbJustificatifs, montantValide, dateModif, idEtat) VALUES('" + uneFicheFrais.UnVisiteur.Id + "','" + uneFicheFrais.Mois + "','" + uneFicheFrais.NbJustificatifs + "','" + uneFicheFrais.MontantValide + "','" + uneFicheFrais.Datemodif.ToString("yyyy-MM-dd") + "','" + uneFicheFrais.UnEtat.Id + "'" + ")";
            this._dbal.Insert(query);
        }

        public void Update(FicheFrais selectedFicheFrais)
        {
            string query = " fichefrais SET idVisiteur = '" + selectedFicheFrais.UnVisiteur.Id + "', mois = '" + selectedFicheFrais.Mois + "', nbJustificatifs = " + selectedFicheFrais.NbJustificatifs + ", montantValide = " + selectedFicheFrais.MontantValide + ", dateModif = '" + selectedFicheFrais.Datemodif.ToString("yyyy-MM-dd") + "', idEtat = '" + selectedFicheFrais.UnEtat.Id + "' WHERE idVisiteur = '" + selectedFicheFrais.UnVisiteur.Id + "' AND mois = '" + selectedFicheFrais.Mois + "'";
            this._dbal.update(query);
        }

        public FicheFrais SelectByVisiteurMois(Visiteur unVisiteur, string moisFiche)
        {
            DataRow r = _dbal.SelectByComposedPK2("fichefrais", "idVisiteur", unVisiteur.Id, "mois", moisFiche);
            if (r != null)
            {
                Etat lEtat = _daoEtat.SelectByIdEtat((string)r["idEtat"]);
                FicheFrais uneFicheFrais = new FicheFrais(moisFiche, (int)r["nbJustificatifs"], (decimal)r["montantValide"], (DateTime)r["dateModif"], unVisiteur, lEtat);
                return uneFicheFrais;
            }
            else
            {
                return null;
            }
        }

        public List<FicheFrais> SelectAll()
        {
            List<FicheFrais> listFicheFrais = new List<FicheFrais>();
            DataTable myTable = this._dbal.SelectAll("fichefrais");
            foreach (DataRow r in myTable.Rows)
            {
                Visiteur leVisiteur = _daoVisiteur.SelectById((string)r["idVisiteur"]);
                Etat lEtat = _daoEtat.SelectByIdEtat((string)r["idEtat"]);
                FicheFrais uneFicheFrais = new FicheFrais((string)r["mois"], (int)r["nbJustificatifs"], (decimal)r["montantValide"], (DateTime)r["dateModif"], leVisiteur, lEtat);
                List<LigneFraisForfait> lesLignesFraisForfaits = new List<LigneFraisForfait>(this._daoLigneFraisForfait.SelectByFicheFrais(uneFicheFrais));
                List<LigneFraisHorsForfait> lesLignesFraisHorsForfait = new List<LigneFraisHorsForfait>(this._daoLigneFraisHorsForfait.SelectByFicheFrais(uneFicheFrais));
                uneFicheFrais.LesLignesFraisForfait = lesLignesFraisForfaits;
                uneFicheFrais.LesLignesFraisHorsForfaits = lesLignesFraisHorsForfait;
                listFicheFrais.Add(uneFicheFrais);
            }
            return listFicheFrais;
        }

        public List<FicheFrais> SelectByMonth(string unMois)
        {
            List<FicheFrais> listFicheFrais = new List<FicheFrais>();
            DataTable myTable = this._dbal.SelectByField("FicheFrais", "mois = '" + unMois + "'");
            foreach (DataRow r in myTable.Rows)
            {
                Visiteur leVisiteur = _daoVisiteur.SelectById((string)r["idVisiteur"]);
                Etat lEtat = _daoEtat.SelectByIdEtat((string)r["idEtat"]);
                FicheFrais uneFicheFrais = new FicheFrais(unMois, (int)r["nbJustificatifs"], (decimal)r["montantValide"], (DateTime)r["dateModif"],leVisiteur, lEtat);
                List<LigneFraisForfait> lesLignesFraisForfaits = new List<LigneFraisForfait>(this._daoLigneFraisForfait.SelectByFicheFrais(uneFicheFrais));
                List<LigneFraisHorsForfait> lesLignesFraisHorsForfait = new List<LigneFraisHorsForfait>(this._daoLigneFraisHorsForfait.SelectByFicheFrais(uneFicheFrais));
                uneFicheFrais.LesLignesFraisForfait = lesLignesFraisForfaits;
                uneFicheFrais.LesLignesFraisHorsForfaits = lesLignesFraisHorsForfait;
                listFicheFrais.Add(uneFicheFrais);
            }
            return listFicheFrais;
        }

       
        public List<string> SelectListMois()
        {
            List<string> listmois = new List<string>();
            DataTable myTable = this._dbal.SelectDistinctByField("mois","FicheFrais", "DESC");
            foreach (DataRow r in myTable.Rows)
            {
                listmois.Add((string)r["mois"]);
            }
            return listmois;
        }
    }
}
