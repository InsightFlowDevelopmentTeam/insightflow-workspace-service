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
    public class WorkspaceController : ControllerBase
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        public WorkspaceController(IWorkspaceRepository workspaceRepository)
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
        public async Task<IActionResult> GetAllWorkspaces()
        {
            var workspaces = await _workspaceRepository.GetAllWorkspacesAsync();
            return Ok(workspaces);
        }
    }
}