using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Enums
{
    public enum Locations
    {
        Abia,
        Adamawa,
        [Display(Name = "Akwa Ibom")]
        AkwaIbom,
        Anambra,
        Bayelsa,
        Bauchi,
        Benue,
        Borno,
        [Display(Name = "Cross River")]
        CrossRiver,
        Delta,
        Enugu,
        Ebonyi,
        Ekiti,
        Edo,
        Jigawa,
        Imo,
        Gombe,
        Kano,
        Kaduna,
        Katsina,
        Kebbi,
        Kogi,
        Kwara,
        Nassarawa,
        Niger,
        Lagos,
        Ondo,
        Ogun,
        Osun,
        Oyo,
        Plateau,
        Rivers,
        Sokoto,
        Taraba,
        Yobe,
        Zamfara,
        FCT
    }
}
