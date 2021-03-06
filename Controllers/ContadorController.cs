﻿using System;
using System.Reflection;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APIContagem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContadorController : ControllerBase
    {
        private static Contador _CONTADOR = new Contador();

        [HttpGet]
        public object Get([FromServices]IConfiguration configuration)
        {
            _CONTADOR.Incrementar();

            lock (_CONTADOR)
            {
                return new
                {
                    _CONTADOR.ValorAtual,
                    Environment.MachineName,
                    Local = "Teste - Azure na Prática",
                    Sistema = Environment.OSVersion.VersionString,
                    Variavel = configuration["TesteAmbiente"],
                    TargetFramework = Assembly
                        .GetEntryAssembly()?
                        .GetCustomAttribute<TargetFrameworkAttribute>()?
                        .FrameworkName
                };
            }
        }
    }
}
