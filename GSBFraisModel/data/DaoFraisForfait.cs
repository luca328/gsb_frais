using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSBFrais.Model.business;

namespace GSBFrais.Model.data
{
    public class DaoFraisForfait
    {
        private object _daoFraisForfait;
        private Dbal _dbal;

        public DaoFraisForfait(Dbal mydbal)
        {
            this._dbal = mydbal;
        }

        public void Insert(FraisForfait unFraisForfait)
        {
            string query = " fraisforfait (id, libelle, montant) VALUES ('" + unFraisForfait.Id + "','" + unFraisForfait.Libelle.Replace("'", "''") + "','" + unFraisForfait.Montant + "')";
            this._dbal.Insert(query);
        }

        public void Update(FraisForfait unFraisForfait)
        {
            string query = " fraisforfait (id, libelle, montant) SET '" + unFraisForfait.Libelle.Replace("'", "''") + "','" + unFraisForfait.Montant + "'";
            this._dbal.update(query);
        }

        public void Delete(FraisForfait unFraisForfait)
        {
            string query = " fraisforfait WHERE id ='" + unFraisForfait.Id + "'";
            this._dbal.delete(query);
        }

        public List<FraisForfait> SelectAll()
        {
            List<FraisForfait> listVisiteurs = new List<FraisForfait>();
            DataTable myTable = this._dbal.SelectAll("fraisforfait");

            foreach (DataRow r in myTable.Rows)
            {
                listVisiteurs.Add(new FraisForfait((string)r["id"], (string)r["libelle"], (decimal)r["montant"]));
            }
            return listVisiteurs;
        }

        public FraisForfait SelectByName(string nameFraisForfait)
        {
            DataTable result = new DataTable();
            result = this._dbal.SelectByField("fraisforfait", "libelle = '" + nameFraisForfait.Replace("'", "''") + "'");
            FraisForfait foundFraisForfait = new FraisForfait((string)result.Rows[0]["id"], (string)result.Rows[0]["libelle"], (decimal)result.Rows[0]["montant"]);
            return foundFraisForfait;
        }

        public FraisForfait SelectById(string idFraisForfais)
        {
            DataRow result = this._dbal.SelectById("fraisforfait", idFraisForfais);
            return new FraisForfait((string)result["id"], (string)result["libelle"], (decimal)result["montant"]);
        }
    }
}
