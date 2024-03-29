﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Client.Models
{
    public partial class Product : BaseModel
    {
        public Product() { }
        public Product(Product product)
        {
            this.naim = product.naim;
            this.dlina = product.dlina;
            this.shir = product.shir;
            this.tol = product.tol;
            this.nazn = product.nazn;
            this.sort = product.sort;
            this.dekor = product.dekor;
            this.zvet = product.zvet;
            this.material = product.material;
            this.manufID = product.manufID;
            this.ves_lista = product.ves_lista;
            this.kol_list_v_pachke = product.kol_list_v_pachke;
            this.CategoryID = product.CategoryID;
        }
        public string naim { get; set; }
        public double dlina { get; set; }
        public double shir { get; set; }
        public double tol { get; set; }
        public string nazn { get; set; }
        public string sort { get; set; }
        public string dekor { get; set; }
        public string zvet { get; set; }
        public string material { get; set; }
        public int? manufID { get; set; }
        public double ves_lista { get; set; }
        public int kol_list_v_pachke { get; set; }
        private int categoryID { get; set; }
        public int CategoryID { get { return categoryID; } set { categoryID = value; OnPropertyChanged(nameof(CategoryID)); } }
        public bool IsEquals(Product t)
        {
            if (t == null) return false;
            if (this == t) return true;
            if (object.ReferenceEquals(this, t)) return true;
            if (t.naim == this.naim && t.sort == this.sort && t.dlina == this.dlina && t.shir == this.shir && t.tol == this.tol/*КАТЕГОРИЯ*/) return true;
            return false;
        }

        public bool IsValid
        {
            get
            {
                return naim != null && dlina != null && shir != null && tol != null && sort != null && nazn != null &&
                    naim != "" && sort != "" && nazn != "";
            }
        }

        public Category Category { get; set; }
        public Manufacture Manufact { get; set; }

        public string Naim2
        {
            get
            {
                return $"{naim} {material} {dlina}*{shir}*{tol} {nazn} {sort} {dekor} {Manufact?.Name}";
            }
        }
    }
}
