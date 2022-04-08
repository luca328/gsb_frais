using GSBFrais.Model.business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFrais.Model.data
{
    public class DaoLigneFraisForfait
    {
        private Dbal _dbal;
        private DaoFraisForfait _daoFraisForfait;

        public DaoLigneFraisForfait(Dbal myDbal, DaoFraisForfait myDaoFraisForfait)
        {
            this._dbal = myDbal;
            this._daoFraisForfait = myDaoFraisForfait;
        }
        /*
        public void Insert(LigneFraisForfait uneLigneFraisForfait)
        {
            string query = " ligneFraisForfait (idFraitForfait, quentite) VALUES ('" + uneLigneFraisForfait.FraisForfait.Id + "','" + uneLigneFraisForfait.Quantite + "')";
            this._dbal.Insert(query);
        }

        public void Update(LigneFraisForfait uneLigneFraisForfait)
        {
            string query = " ligneFraisForfait (idVisiteur, mois, idFraitForfait, quentite) SET '" + uneLigneFraisForfait.FraisForfait.Id + "','" + uneLigneFraisForfait.Quantite + "'";
            this._dbal.update(query);
        }

        public void Delete(LigneFraisForfait uneLigneFraisForfait)
        {
            string query = " visiteur WHERE idVisiteur ='" + "'AND idFraitForfait ='" + uneLigneFraisForfait.FraisForfait.Id + "'";
            this._dbal.delete(query);
        }
        */
        public List<LigneFraisForfait> SelectAll()
        {
            List<LigneFraisForfait> listLigneFraisForfait = new List<LigneFraisForfait>();
            DataTable myTable = this._dbal.SelectAll("ligneFraisForfait");

            foreach (DataRow r in myTable.Rows)
            {
                listLigneFraisForfait.Add(new LigneFraisForfait((FraisForfait)r["FraisForfait"], (int)r["quantite"], (FicheFrais)r["ficheFrais"]));
            }
            return listLigneFraisForfait;
        }

        public LigneFraisForfait SelectById(string idFraisForfait)
        {
            DataRow result = this._dbal.SelectById("ligneFraisForfait", "idFraisForfait = " + idFraisForfait);
            return new LigneFraisForfait((FraisForfait)result["FraisForfait"], (int)result["quantite"], (FicheFrais)result["ficheFrais"]);
        }

        public List<LigneFraisForfait> SelectByFicheFrais(FicheFrais uneFicheFrais)
        {
            List<LigneFraisForfait> listFraisForfait = new List<LigneFraisForfait>();
            DataTable myTable = this._dbal.SelectByComposedFK2("LigneFraisForfait", "idVisiteur", uneFicheFrais.UnVisiteur.Id, "Mois",uneFicheFrais.Mois);
            foreach (DataRow r in myTable.Rows)
            {
                FraisForfait fraisForfait = this._daoFraisForfait.SelectById((string)r["idFraisForfait"]);
                listFraisForfait.Add(new LigneFraisForfait(fraisForfait, (int)r["quantite"], uneFicheFrais));
            }
            return listFraisForfait;
        }
    }
}
