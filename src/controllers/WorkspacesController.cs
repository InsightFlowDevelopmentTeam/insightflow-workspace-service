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
            return Ok("Workspace created successfully.");
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllWorkspacesByUser([FromQuery] Guid userId)
        {
            var workspaces = await _workspaceRepository.GetAllWorkspacesByUserAsync(userId);
            return Ok(workspaces);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkspaceById([FromRoute] Guid id)
        {
            var workspaces = await _workspaceRepository.GetWorkspaceByIdAsync(id);
            return Ok(workspaces);
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