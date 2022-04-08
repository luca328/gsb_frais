using GSBFrais.Model.business;
using GSBFrais.Model.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFrais.Model.data
{
    public class DaoLigneFraisHorsForfait
    {
        private Dbal _dbal;

        public DaoLigneFraisHorsForfait(Dbal mydbal)
        {
            this._dbal = mydbal;
        }
        /*
        public void Insert(LigneFraisHorsForfait LigneFraisHorsForfait)
        {
            string query = " ligneFraisForfait (idVisiteur, mois, idFraitForfait, quentite) VALUES ('" + LigneFraisHorsForfait.Id + "',' " + LigneFraisHorsForfait.Visiteur.Id + "','" + LigneFraisHorsForfait.Mois + "','" + LigneFraisHorsForfait.Libelle + "','" + LigneFraisHorsForfait.Date + "','" + LigneFraisHorsForfait.Montant + "')";
            this._dbal.Insert(query);
        }
        */
        public void Update(LigneFraisHorsForfait LigneFraisHorsForfait)
        {
            string query = "lignefraishorsforfait SET idVisiteur= '" + LigneFraisHorsForfait.Fichefrais.UnVisiteur.Id + "', mois = '" + LigneFraisHorsForfait.Fichefrais.Mois + "', libelle='" + LigneFraisHorsForfait.Libelle + "', date= '" + LigneFraisHorsForfait.Date.ToString("yyyy-MM-dd") + "',montant='" + LigneFraisHorsForfait.Montant + "'" + " WHERE id = '" + LigneFraisHorsForfait.Id + "'"; ;
            this._dbal.update(query);
        }

        public void Delete(LigneFraisHorsForfait LigneFraisHorsForfait)
        {
            string query = " LigneFraisHorsForfait WHERE id ='" + LigneFraisHorsForfait.Id + "'AND id ='" + LigneFraisHorsForfait.Id + "'";
            this._dbal.delete(query);
        }

        public List<LigneFraisHorsForfait> SelectAll(FicheFrais uneFicheFrais)
        {
            List<LigneFraisHorsForfait> listLigneFraisForfait = new List<LigneFraisHorsForfait>();
            DataTable myTable = this._dbal.SelectAll("ligneFraisHorsForfait");

            foreach (DataRow r in myTable.Rows)
            {
                listLigneFraisForfait.Add(new LigneFraisHorsForfait((int)r["id"], (string)r["libelle"], (DateTime)r["date"], (decimal)r["montant"], uneFicheFrais));
            }
            return listLigneFraisForfait;
        }

        public LigneFraisHorsForfait SelectById(string idFraisHorsForfait, FicheFrais uneFicheFrais)
        {
            DataRow result = this._dbal.SelectById("ligneFraisHorsForfait", "id = " + idFraisHorsForfait);
            return new LigneFraisHorsForfait((int)result["id"], (string)result["libelle"], (DateTime)result["date"], (decimal)result["montant"], uneFicheFrais);
        }
        public List<LigneFraisHorsForfait> SelectByFicheFrais(FicheFrais uneFicheFrais)
        {
            List<LigneFraisHorsForfait> listFraisForfait = new List<LigneFraisHorsForfait>();
            DataTable myTable = this._dbal.SelectByComposedFK2("lignefraishorsforfait", "idVisiteur", uneFicheFrais.UnVisiteur.Id, "mois", uneFicheFrais.Mois);
            foreach (DataRow r in myTable.Rows)
            {
                listFraisForfait.Add(new LigneFraisHorsForfait((int)r["id"], (string)r["libelle"], (DateTime)r["date"], (decimal)r["montant"], uneFicheFrais));
            }
            return listFraisForfait;
        }
    }
}
