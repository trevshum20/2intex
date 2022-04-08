using Intex2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Controllers
{
    public class InferenceController : Controller
    {
        private InferenceSession _session;

        public InferenceController(InferenceSession session)
        {
            _session = session;
        }

        
        [HttpGet]
        [Authorize]
        public IActionResult Calculator()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Calculator(CalcData data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() };
            result.Dispose();
            ViewBag.result = prediction.PredictedValue.ToString("0");
            return View("Calculator", data);
        }

        public IActionResult Populate (
            string city, string county, bool ped,
            bool bike, bool motor, bool imp,
            bool unr, bool inter, bool dui, 
            bool wild, bool over, bool comm, 
            bool old, bool single, bool dist,
            bool drows, bool road)   
        {
            var data = new CalcData();

            if (city is "SALT LAKE CITY")
            {
                data.city_Other = 0;
            }
            else if ( city is "WEBER VALLEY CITY" )
            {
                data.city_Other = 0;
            }
            else { data.city_Other = 1; }

            if (county is "UTAH")
            {
                data.county_name_Other = 0;
            }
            else if (county is "HEBER")
            {
                data.county_name_Other = 0;
            }
            else if (county is "DAVIS")
            {
                data.county_name_Other = 0;
            }
            else { data.county_name_Other = 1; }

            if (ped is true)
            {
                data.pedestrian_involved_True = 1;
            }
            else { data.pedestrian_involved_True = 0; }

            if (bike is true)
            {
                data.bicyclist_involved_True = 1;
            }
            else { data.bicyclist_involved_True = 0; }

            if (motor is true)
            {
                data.motorcycle_involved_True = 1;
            }
            else { data.motorcycle_involved_True = 0; }

            if (imp is true)
            {
                data.improper_restraint_True = 1;
            }
            else { data.improper_restraint_True = 0; }

            if (unr is true)
            {
                data.unrestrained_True = 1;
            }
            else { data.improper_restraint_True = 0; }

            if (inter is true)
            {
                data.intersection_related_True = 1;
            }
            else { data.intersection_related_True = 0; }

            if (dui is true)
            {
                data.dui_True = 1;
            }
            else { data.dui_True = 0; }

            if (wild is true)
            {
                data.wild_animal_related_True = 1;
            }
            else { data.wild_animal_related_True = 0; }

            if (over is true)
            {
                data.overturn_rollover_True = 1;
            }
            else { data.overturn_rollover_True = 0; }

            if (comm is true)
            {
                data.commercial_motor_veh_involved_True = 1;
            }
            else { data.commercial_motor_veh_involved_True = 0; }

            if (old is true)
            {
                data.older_driver_involved_True = 1;
            }
            else { data.older_driver_involved_True = 0; }

            if (single is true)
            {
                data.single_vehicle_True = 1;
            }
            else { data.single_vehicle_True = 0; }

            if (dist is true)
            {
                data.distracted_driving_True = 1;
            }
            else { data.distracted_driving_True = 0; }

            if (drows is true)
            {
                data.drowsy_driving_True = 1;
            }
            else { data.drowsy_driving_True = 0; }

            if (road is true)
            {
                data.roadway_departure_True = 1;
            }
            else { data.roadway_departure_True = 0; }

            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() };
            result.Dispose();
            ViewBag.result = prediction.PredictedValue.ToString("0");

            return View("Calculator", data);
        }
    }
}
