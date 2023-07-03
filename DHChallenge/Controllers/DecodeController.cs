using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DHChallenge.Controllers
{
    public class DecodeController : Controller
    {
        public IActionResult Index(string txtMsg)
        {
            ViewData["Result"] = GetMessage(txtMsg);
            return View();
        }

        private string GetMessage(string msg)
        {
            if (!String.IsNullOrEmpty(msg))
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                string pattern = @"^(.*?)0*([^0]+)0*([^0]+)$";
                Match match = Regex.Match(msg, pattern);

                //Vaidamos si cumple con el patron
                if (!match.Success)
                {
                    throw new ArgumentException("La cadena proporcionada no tiene el formato esperado.");
                }

                // Validamos de que la cadena tenga el formato correcto
                if (match.Groups.Count != 4)
                {
                    throw new ArgumentException("La cadena proporcionada no tiene el formato esperado.");
                }

                // Asignamos los valores al diccionario
                dictionary["first_name"] = match.Groups[1].Value;
                dictionary["last_name"] = match.Groups[2].Value;
                dictionary["id"] = match.Groups[3].Value;

                ViewData["Result"] = JsonConvert.SerializeObject(dictionary);               
                return JsonConvert.SerializeObject(dictionary);
            }
            else
            {
                throw new ArgumentException("El mensaje no puede ser vacio.");
            }

        }
    }
}
