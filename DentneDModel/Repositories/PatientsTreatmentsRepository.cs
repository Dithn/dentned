﻿#region License
// Copyright (c) 2015 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using DG.Data.Model;
using DG.DentneD.Model.Entity;
using System.Linq;

namespace DG.DentneD.Model.Repositories
{
    public class PatientsTreatmentsRepository : GenericDataRepository<patientstreatments, DentneDModel>
    {
        public PatientsTreatmentsRepository() : base() { }

        /// <summary>
        /// Repository language dictionary
        /// </summary>
        public class RepositoryLanguage : IGenericDataRepositoryLanguage
        {
            public string text001 = "Doctors is mandatory.";
            public string text002 = "Patient is mandatory.";
            public string text003 = "Treatments type is mandatory.";
            public string text004 = "Invalid sex. Can be 'M' or 'F'.";
            public string text005 = "Invalid tax rate. Can not be less than zero.";
            public string text101 = "Arches";
            public string text102 = "Upper Arch";
            public string text103 = "Lower Arch";
            public string text104 = "None";
        }

        /// <summary>
        /// Repository language
        /// </summary>
        public RepositoryLanguage language = new RepositoryLanguage();

        /// <summary>
        /// Check if an item can be added
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public override bool CanAdd(ref string[] errors, params patientstreatments[] items)
        {
            errors = new string[] { };

            bool ret = Validate(false, ref errors, items);
            if (!ret)
                return ret;

            ret = base.CanAdd(ref errors, items);

            return ret;
        }

        /// <summary>
        /// Check if an item can be updated
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public override bool CanUpdate(ref string[] errors, params patientstreatments[] items)
        {
            errors = new string[] { };

            bool ret = Validate(true, ref errors, items);
            if (!ret)
                return ret;

            ret = base.CanUpdate(ref errors, items);

            return ret;
        }

        /// <summary>
        /// Validate an item
        /// </summary>
        /// <param name="isUpdate"></param>
        /// <param name="errors"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        private bool Validate(bool isUpdate, ref string[] errors, params patientstreatments[] items)
        {
            bool ret = true;

            foreach (patientstreatments item in items)
            {
                if (item.patientstreatments_taxrate < 0)
                {
                    ret = false;
                    errors = errors.Concat(new string[] { language.text005 }).ToArray();
                }

                if (!ret)
                    break;

                if (BaseModel.Doctors.Find(item.doctors_id) == null)
                {
                    ret = false;
                    errors = errors.Concat(new string[] { language.text001 }).ToArray();
                }
                if (BaseModel.Patients.Find(item.patients_id) == null)
                {
                    ret = false;
                    errors = errors.Concat(new string[] { language.text002 }).ToArray();
                }
                if (BaseModel.Treatments.Find(item.treatments_id) == null)
                {
                    ret = false;
                    errors = errors.Concat(new string[] { language.text003 }).ToArray();
                }

                if (!ret)
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Check if an item can be removed
        /// </summary>
        /// <param name="checkForeingKeys"></param>
        /// <param name="excludedForeingKeys"></param>
        /// <param name="errors"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public override bool CanRemove(bool checkForeingKeys, string[] excludedForeingKeys, ref string[] errors, params patientstreatments[] items)
        {
            bool ret = true;

            errors = new string[] { };

            if (excludedForeingKeys == null)
                excludedForeingKeys = new string[] { };
            if (!excludedForeingKeys.Contains("FK_estimateslines_patientstreatments"))
                excludedForeingKeys = excludedForeingKeys.Concat(new string[] { "FK_estimateslines_patientstreatments" }).ToArray();
            if (!excludedForeingKeys.Contains("FK_invoiceslines_patientstreatments"))
                excludedForeingKeys = excludedForeingKeys.Concat(new string[] { "FK_invoiceslines_patientstreatments" }).ToArray();

            ret = base.CanRemove(checkForeingKeys, excludedForeingKeys, ref errors, items);

            return ret;
        }

        /// <summary>
        /// Remove an item
        /// </summary>
        /// <param name="items"></param>
        public override void Remove(params patientstreatments[] items)
        {
            //remove or unset all related items
            foreach (patientstreatments item in items)
            {
                foreach (invoiceslines invoiceline in BaseModel.InvoicesLines.List(r => r.patientstreatments_id == item.patientstreatments_id))
                {
                    invoiceline.patientstreatments_id = null;
                    BaseModel.InvoicesLines.Update(invoiceline);
                }
                foreach (estimateslines estimateline in BaseModel.EstimatesLines.List(r => r.patientstreatments_id == item.patientstreatments_id))
                {
                    estimateline.patientstreatments_id = null;
                    BaseModel.EstimatesLines.Update(estimateline);
                }
            }

            base.Remove(items);
        }

        /// <summary>
        /// Get the tooth string for a treatment
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetTreatmentsToothsString(patientstreatments item)
        {
            string ret = (item.patientstreatments_t11 && item.patientstreatments_t12 && item.patientstreatments_t13 && item.patientstreatments_t14 && item.patientstreatments_t15 && item.patientstreatments_t16 && item.patientstreatments_t17 && item.patientstreatments_t18 &&
                    item.patientstreatments_t21 && item.patientstreatments_t22 && item.patientstreatments_t23 && item.patientstreatments_t24 && item.patientstreatments_t25 && item.patientstreatments_t26 && item.patientstreatments_t27 && item.patientstreatments_t28 &&
                    item.patientstreatments_t31 && item.patientstreatments_t32 && item.patientstreatments_t33 && item.patientstreatments_t34 && item.patientstreatments_t35 && item.patientstreatments_t36 && item.patientstreatments_t37 && item.patientstreatments_t38 &&
                    item.patientstreatments_t41 && item.patientstreatments_t42 && item.patientstreatments_t43 && item.patientstreatments_t44 && item.patientstreatments_t45 && item.patientstreatments_t46 && item.patientstreatments_t47 && item.patientstreatments_t48 ? language.text101 :
                        (item.patientstreatments_t11 && item.patientstreatments_t12 && item.patientstreatments_t13 && item.patientstreatments_t14 && item.patientstreatments_t15 && item.patientstreatments_t16 && item.patientstreatments_t17 && item.patientstreatments_t18 &&
                        item.patientstreatments_t21 && item.patientstreatments_t22 && item.patientstreatments_t23 && item.patientstreatments_t24 && item.patientstreatments_t25 && item.patientstreatments_t26 && item.patientstreatments_t27 && item.patientstreatments_t28 &&
                        !item.patientstreatments_t31 && !item.patientstreatments_t32 && !item.patientstreatments_t33 && !item.patientstreatments_t34 && !item.patientstreatments_t35 && !item.patientstreatments_t36 && !item.patientstreatments_t37 && !item.patientstreatments_t38 &&
                        !item.patientstreatments_t41 && !item.patientstreatments_t42 && !item.patientstreatments_t43 && !item.patientstreatments_t44 && !item.patientstreatments_t45 && !item.patientstreatments_t46 && !item.patientstreatments_t47 && !item.patientstreatments_t48 ? language.text102 :
                            (!item.patientstreatments_t11 && !item.patientstreatments_t12 && !item.patientstreatments_t13 && !item.patientstreatments_t14 && !item.patientstreatments_t15 && !item.patientstreatments_t16 && !item.patientstreatments_t17 && !item.patientstreatments_t18 &&
                            !item.patientstreatments_t21 && !item.patientstreatments_t22 && !item.patientstreatments_t23 && !item.patientstreatments_t24 && !item.patientstreatments_t25 && !item.patientstreatments_t26 && !item.patientstreatments_t27 && !item.patientstreatments_t28 &&
                            item.patientstreatments_t31 && item.patientstreatments_t32 && item.patientstreatments_t33 && item.patientstreatments_t34 && item.patientstreatments_t35 && item.patientstreatments_t36 && item.patientstreatments_t37 && item.patientstreatments_t38 &&
                            item.patientstreatments_t41 && item.patientstreatments_t42 && item.patientstreatments_t43 && item.patientstreatments_t44 && item.patientstreatments_t45 && item.patientstreatments_t46 && item.patientstreatments_t47 && item.patientstreatments_t48 ? language.text103 :
                                (!item.patientstreatments_t11 && !item.patientstreatments_t12 && !item.patientstreatments_t13 && !item.patientstreatments_t14 && !item.patientstreatments_t15 && !item.patientstreatments_t16 && !item.patientstreatments_t17 && !item.patientstreatments_t18 &&
                                !item.patientstreatments_t21 && !item.patientstreatments_t22 && !item.patientstreatments_t23 && !item.patientstreatments_t24 && !item.patientstreatments_t25 && !item.patientstreatments_t26 && !item.patientstreatments_t27 && !item.patientstreatments_t28 &&
                                !item.patientstreatments_t31 && !item.patientstreatments_t32 && !item.patientstreatments_t33 && !item.patientstreatments_t34 && !item.patientstreatments_t35 && !item.patientstreatments_t36 && !item.patientstreatments_t37 && !item.patientstreatments_t38 &&
                                !item.patientstreatments_t41 && !item.patientstreatments_t42 && !item.patientstreatments_t43 && !item.patientstreatments_t44 && !item.patientstreatments_t45 && !item.patientstreatments_t46 && !item.patientstreatments_t47 && !item.patientstreatments_t48 ? language.text104 :
                                    (item.patientstreatments_t11 ? "11," : "") +
                                    (item.patientstreatments_t12 ? "12," : "") +
                                    (item.patientstreatments_t13 ? "13," : "") +
                                    (item.patientstreatments_t14 ? "14," : "") +
                                    (item.patientstreatments_t15 ? "15," : "") +
                                    (item.patientstreatments_t16 ? "16," : "") +
                                    (item.patientstreatments_t17 ? "17," : "") +
                                    (item.patientstreatments_t18 ? "18," : "") +
                                    (item.patientstreatments_t21 ? "21," : "") +
                                    (item.patientstreatments_t22 ? "22," : "") +
                                    (item.patientstreatments_t23 ? "23," : "") +
                                    (item.patientstreatments_t24 ? "24," : "") +
                                    (item.patientstreatments_t25 ? "25," : "") +
                                    (item.patientstreatments_t26 ? "26," : "") +
                                    (item.patientstreatments_t27 ? "27," : "") +
                                    (item.patientstreatments_t28 ? "28," : "") +
                                    (item.patientstreatments_t31 ? "31," : "") +
                                    (item.patientstreatments_t32 ? "32," : "") +
                                    (item.patientstreatments_t33 ? "33," : "") +
                                    (item.patientstreatments_t34 ? "34," : "") +
                                    (item.patientstreatments_t35 ? "35," : "") +
                                    (item.patientstreatments_t36 ? "36," : "") +
                                    (item.patientstreatments_t37 ? "37," : "") +
                                    (item.patientstreatments_t38 ? "38," : "") +
                                    (item.patientstreatments_t41 ? "41," : "") +
                                    (item.patientstreatments_t42 ? "42," : "") +
                                    (item.patientstreatments_t43 ? "43," : "") +
                                    (item.patientstreatments_t44 ? "44," : "") +
                                    (item.patientstreatments_t45 ? "45," : "") +
                                    (item.patientstreatments_t46 ? "46," : "") +
                                    (item.patientstreatments_t47 ? "47," : "") +
                                    (item.patientstreatments_t48 ? "48," : "")
                                )
                            )
                        )
                    );
            ret = (ret.EndsWith(",") ? ret.Substring(0, ret.Length - 1) : ret);
            return ret;
        }

        /// <summary>
        /// Get the number of tooth for a treatment
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int GetNumberOfTooths(patientstreatments item)
        {
            int ret = 0;
            ret += (item.patientstreatments_t11 ? 1 : 0);
            ret += (item.patientstreatments_t12 ? 1 : 0);
            ret += (item.patientstreatments_t13 ? 1 : 0);
            ret += (item.patientstreatments_t14 ? 1 : 0);
            ret += (item.patientstreatments_t15 ? 1 : 0);
            ret += (item.patientstreatments_t16 ? 1 : 0);
            ret += (item.patientstreatments_t17 ? 1 : 0);
            ret += (item.patientstreatments_t18 ? 1 : 0);
            ret += (item.patientstreatments_t21 ? 1 : 0);
            ret += (item.patientstreatments_t22 ? 1 : 0);
            ret += (item.patientstreatments_t23 ? 1 : 0);
            ret += (item.patientstreatments_t24 ? 1 : 0);
            ret += (item.patientstreatments_t25 ? 1 : 0);
            ret += (item.patientstreatments_t26 ? 1 : 0);
            ret += (item.patientstreatments_t27 ? 1 : 0);
            ret += (item.patientstreatments_t28 ? 1 : 0);
            ret += (item.patientstreatments_t31 ? 1 : 0);
            ret += (item.patientstreatments_t32 ? 1 : 0);
            ret += (item.patientstreatments_t33 ? 1 : 0);
            ret += (item.patientstreatments_t34 ? 1 : 0);
            ret += (item.patientstreatments_t35 ? 1 : 0);
            ret += (item.patientstreatments_t36 ? 1 : 0);
            ret += (item.patientstreatments_t37 ? 1 : 0);
            ret += (item.patientstreatments_t38 ? 1 : 0);
            ret += (item.patientstreatments_t41 ? 1 : 0);
            ret += (item.patientstreatments_t42 ? 1 : 0);
            ret += (item.patientstreatments_t43 ? 1 : 0);
            ret += (item.patientstreatments_t44 ? 1 : 0);
            ret += (item.patientstreatments_t45 ? 1 : 0);
            ret += (item.patientstreatments_t46 ? 1 : 0);
            ret += (item.patientstreatments_t47 ? 1 : 0);
            ret += (item.patientstreatments_t48 ? 1 : 0);
            return ret;
        }
    }

}

