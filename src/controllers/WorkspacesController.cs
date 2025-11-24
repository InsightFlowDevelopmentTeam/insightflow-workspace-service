using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.dtos;
using insightflow_workspace_service.src.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace insightflow_workspace_service.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkspacesController : ControllerBase
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        public WorkspacesController(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateWorkspace([FromForm] CreateWorkspaceDTO createWorkspaceDTO
        )
        {
            var result = await _workspaceRepository.CreateWorkspaceAsync(createWorkspaceDTO);
            if (!result)
            {
                return BadRequest("Workspace creation failed.");
            }
            return Ok(createWorkspaceDTO);
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllWorkspacesByUser([FromQuery] Guid userId)
        {
            var workspaces = await _workspaceRepository.GetAllWorkspacesByUserAsync(userId);
            if (workspaces == null || !workspaces.Any())
            {
                return NoContent();
            }
            return Ok(workspaces);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkspaceById([FromRoute] Guid id)
        {
            var workspaces = await _workspaceRepository.GetWorkspaceByIdAsync(id);
            return Ok(workspaces);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateWorkspace([FromRoute] Guid id, [FromForm] UpdateWorkspaceDTO updateWorkspaceDTO)
        {
            var result = await _workspaceRepository.UpdateWorkspaceAsync(id, updateWorkspaceDTO);
            if (!result)
            {
                return BadRequest("Workspace update failed.");
            }
            return Ok(updateWorkspaceDTO);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkspace([FromRoute] Guid id)
        {
            var result = await _workspaceRepository.DeleteWorkspaceAsync(id);
            if (!result)
            {
                return BadRequest("Workspace deletion failed.");
            }
            return Ok();
        }
        // solo debug
        [HttpGet("all")]
        public async Task<IActionResult> GetAllWorkspaces()
        {
            var workspaces = await _workspaceRepository.GetAllWorkspacesAsync();
            return Ok(workspaces);
        }
    }
}