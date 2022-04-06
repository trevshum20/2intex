using Intex2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public class CrashData : Controller
    {
        public float city_Other { get; set; }

        public float county_name_Other { get; set; }

        public float pedestrian_involved_True { get; set; }

        public float bicyclist_involved_True { get; set; }

        public float motorcycle_involved_True { get; set; }

        public float improper_restraint_True { get; set; }

        public float unrestrained_True { get; set; }

        public float dui_True { get; set; }

        public float intersection_related_True { get; set; }

        public float wild_animal_related_True { get; set; }

        public float overturn_rollover_True { get; set; }

        public float commercial_motor_veh_involved_True { get; set; }

        public float older_driver_involved_True { get; set; }

        public float single_vehicle_True { get; set; }

        public float distracted_driving_True { get; set; }

        public float drowsy_driving_True { get; set; }

        public float roadway_departure_True { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                city_Other, county_name_Other,
                pedestrian_involved_True,
                bicyclist_involved_True,
                motorcycle_involved_True,
                improper_restraint_True,
                unrestrained_True, dui_True,
                intersection_related_True,
                wild_animal_related_True,
                overturn_rollover_True,
                commercial_motor_veh_involved_True,
                older_driver_involved_True,
                single_vehicle_True,
                distracted_driving_True,
                drowsy_driving_True,
                roadway_departure_True
            };
            int[] dimensions = new int[] { 1, 17 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}
