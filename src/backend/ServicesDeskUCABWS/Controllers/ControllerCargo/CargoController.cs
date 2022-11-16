﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServicesDeskUCABWS.BussinesLogic.DAO.CargoDAO;
using ServicesDeskUCABWS.BussinesLogic.DTO.CargoDTO;
using ServicesDeskUCABWS.BussinesLogic.Mapper;
using ServicesDeskUCABWS.Data;
using ServicesDeskUCABWS.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace ServicesDeskUCABWS.Controllers.ControllerCargo
{
    [Route("Cargo")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly ICargoDAO _cargoDAO;
        private readonly ILogger<CargoController> _log;

        //Constructor
        public CargoController(ICargoDAO cargoDAO, ILogger<CargoController> log)
        {
            _cargoDAO = cargoDAO;
            _log = log;
        }

        //Crear Cargo
        [HttpPost]
        [Route("CrearCargo/")]
        public CargoDto CrearCargo([FromBody] CargoDto dto1)
        {
            try
            {
                var dao = _cargoDAO.AgregarCargoDAO(CargoMapper.MapperDTOToEntity(dto1));
                return dao;

            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        [HttpGet]
        [Route("consultarcargo/")]
        public ActionResult<List<CargoDto>> ConsultarCargos()
        {
            try
            {
                return _cargoDAO.ConsultarCargos();
            }
            catch (Exception ex)
            {

                throw ex.InnerException!;
            }
        }

        [HttpGet]
        [Route("consultarcargoporid/{id}")]
        public ActionResult<CargoDto> ConsultarPorID([FromRoute] Guid id)
        {
            try
            {
                return _cargoDAO.ConsultarPorID(id);
            }
            catch (Exception ex)
            {

                throw ex.InnerException!;
            }
        }

        [HttpDelete]
        [Route("eliminarcargo/{id}")]
        public ActionResult<CargoDto> EliminarCargo([FromRoute] Guid id)
        {
            try
            {
                return _cargoDAO.eliminarCargo(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }
    }
}