using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.dtos;
using insightflow_workspace_service.src.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace insightflow_workspace_service.src.controllers
{
    /// <summary>
    /// Controlador de workspaces.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WorkspacesController : ControllerBase
    {
        /// <summary>
        /// Repositorio de workspaces.
        /// </summary>
        private readonly IWorkspaceRepository _workspaceRepository;
        /// <summary>
        /// Constructor del controlador de workspaces.
        /// </summary>
        /// <param name="workspaceRepository"></param>
        public WorkspacesController(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }
        /// <summary>
        /// Método para crear un nuevo workspace.
        /// </summary>
        /// <param name="createWorkspaceDTO"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<IActionResult> CreateWorkspace([FromForm] CreateWorkspaceDTO createWorkspaceDTO
        )
        {
            // Crear el workspace
            var result = await _workspaceRepository.CreateWorkspaceAsync(createWorkspaceDTO);
            // Verificar si la creación fue exitosa
            if (!result)
            {
                return BadRequest("Workspace creation failed.");
            }
            // Devolver el workspace creado
            return Ok(createWorkspaceDTO);
        }
        /// <summary>
        /// Método para obtener todos los workspaces de un usuario.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> GetAllWorkspacesByUser([FromQuery] Guid userId)
        {
            // Obtener los workspaces del usuario
            var workspaces = await _workspaceRepository.GetAllWorkspacesByUserAsync(userId);
            // Verificar si se encontraron workspaces
            if (workspaces == null || !workspaces.Any())
            {
                return NoContent();
            }
            // Devolver los workspaces al cliente
            return Ok(workspaces);
        }
        /// <summary>
        /// Método para obtener un workspace por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkspaceById([FromRoute] Guid id)
        {
            // Obtener el workspace por su ID
            var workspaces = await _workspaceRepository.GetWorkspaceByIdAsync(id);
            // Retornar el workspace
            return Ok(workspaces);
        }
        /// <summary>
        /// Método para actualizar un workspace.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateWorkspaceDTO"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateWorkspace([FromRoute] Guid id, [FromForm] UpdateWorkspaceDTO updateWorkspaceDTO)
        {
            // Actualizar el workspace
            var result = await _workspaceRepository.UpdateWorkspaceAsync(id, updateWorkspaceDTO);
            // Verificar si la actualización fue exitosa
            if (!result)
            {
                return BadRequest("Workspace update failed.");
            }
            // Devolver el workspace actualizado
            return Ok(updateWorkspaceDTO);
        }
        /// <summary>
        /// Método para eliminar un workspace.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkspace([FromRoute] Guid id)
        {
            // Eliminar el workspace
            var result = await _workspaceRepository.DeleteWorkspaceAsync(id);
            // Verificar si la eliminación fue exitosa
            if (!result)
            {
                return BadRequest("Workspace deletion failed.");
            }
            // Devolver una respuesta exitosa
            return Ok();
        }
        /// <summary>
        /// Método para obtener todos los workspaces.
        /// Este método es para propósitos de depuración.
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllWorkspaces()
        {
            // Obtener todos los workspaces
            var workspaces = await _workspaceRepository.GetAllWorkspacesAsync();
            // Devolver los workspaces
            return Ok(workspaces);
        }
    }
}