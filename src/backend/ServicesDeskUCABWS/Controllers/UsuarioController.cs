﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServicesDeskUCABWS.BussinesLogic.DAO.LoginDAO;
using ServicesDeskUCABWS.BussinesLogic.DAO.UsuarioDAO;
using ServicesDeskUCABWS.BussinesLogic.DTO.Usuario;
using ServicesDeskUCABWS.BussinesLogic.Exceptions;
using ServicesDeskUCABWS.BussinesLogic.Mapper.UserMapper;
using ServicesDeskUCABWS.BussinesLogic.Response;
using ServicesDeskUCABWS.Data;
using ServicesDeskUCABWS.Entities;
using ServicesDeskUCABWS.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesDeskUCABWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUsuarioDAO _usuarioDAO;
        private readonly IUserLoginDAO _userLoginDAO;
        private readonly ILogger<UsuarioController> _log;


        public UsuarioController(IUsuarioDAO usuarioDao, ILogger<UsuarioController> log, IUserLoginDAO userLogin)
        {
            _usuarioDAO = usuarioDao;
            _log = log;
            _dataContext = dataContext;
            _userLoginDAO = userLogin; 
        }


      
        [HttpGet]
        public ApplicationResponse<List<Usuario>> ConsultarUsuarios()
        {
            var response = new ApplicationResponse<List<Usuario>>();
            try
            {
                response.Data= _usuarioDAO.ObtenerUsuarios();
            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        [HttpPost]
        [Route("CrearAdministrador/")]
        public ApplicationResponse<Administrador> CrearAdministrador([FromBody] UsuarioDto Usuario)
        {
            var response = new ApplicationResponse<Administrador>();
            try
            {
                var resultService = _usuarioDAO.AgregarAdminstrador(UserMapper.MapperEntityToDtoAdmin(Usuario));
                response.Data = resultService;

            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
        [HttpPost]
        [Authorize]
        [Route("CrearCliente/")]
        public ApplicationResponse<Cliente> CrearCliente([FromBody] UsuarioDto Usuario)
        {
            var response = new ApplicationResponse<Cliente>();
            try
            {
                response.Data = _usuarioDAO.AgregarCliente(UserMapper.MapperEntityToDtoClient(Usuario));

            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        [HttpPost]
        [Route("CrearEmpleado/")]
        public ApplicationResponse<Empleado> CrearEmpleado([FromBody] UsuarioDto Usuario)
        {
            var response = new ApplicationResponse<Empleado>();
            try
            {
                var resultService = _usuarioDAO.AgregarEmpleado(UserMapper.MapperEntityToDtoEmp(Usuario));
                response.Data = resultService;

            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        [HttpPost]
        [Route("RecuperarClave/{Gmail}")]
        public ActionResult<string> RecuperarClave([FromRoute] string Gmail)
        {
            try
            {
                _usuarioDAO.RecuperarClave(Gmail);
                return "Mensaje enviado";
            }
            catch (ExceptionsControl ex)
            {
                return "El correo no existe";
            }

        }

        [HttpGet]
        [Route("Consulta/Usuario/{id}")]
        public ApplicationResponse<Usuario> GetByTipoEstadoIdCtrl(Guid id)
        {
            var response = new ApplicationResponse<Usuario>();
            try
            {
                response.Data = _usuarioDAO.consularUsuarioID(id);
            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        [HttpDelete]
        [Route("EliminarUsuario/{id}")]
        public ApplicationResponse<UsuarioDto> EliminarUsuario([FromRoute] Guid id)
        {
            var response = new ApplicationResponse<UsuarioDto>();
            try
            {
                var resultService = _usuarioDAO.eliminarUsuario(id);
                response.Data = resultService;

            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        [HttpPut]
   
        [Route("ActualizarUsuario/")]
        public ApplicationResponse<UserDto_Update> ActualizarUsuario([FromBody] UserDto_Update usuario)
        {
            var response = new ApplicationResponse<UserDto_Update>();
            try
            {
                var resultService = _usuarioDAO.ActualizarUsuario(UserMapper.MapperEntityToDtoUpdate(usuario));
                response.Data = resultService;

            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        [HttpPut]
        [Route("ActualizarUsuarioPassword/")]
        public ApplicationResponse<String> ActualizarPassword([FromBody] UserPasswordDto usuario)
        {
            var response = new ApplicationResponse<String>();
            try
            {
                var resultService = _usuarioDAO.ActualizarUsuarioPassword(UserMapper.MapperEntityToDtoUpdatePassword(usuario));
                response.Data = resultService.ToString();

            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        [HttpPost]
        [Route("login/")]
        public ActionResult<UserResponseLoginDTO> UserLogin([FromBody]  UserLoginDto usuario )
        {
            try
            {
         
                  return _userLoginDAO.UserLogin(usuario);
            }
            catch (Exception e )
            {
                Console.WriteLine(e.Message);

                throw;
            }
        }
    }
}
