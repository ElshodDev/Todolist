//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Todolist.Api.Models.Foundations.TaskItems;
using Todolist.Api.Models.Foundations.TaskItems.Exceptions;
using Todolist.Api.Services.Foundations.TaskItems;

namespace Todolist.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : RESTFulController
    {
        private readonly ITaskItemService taskItemService;

        public TaskItemsController(ITaskItemService taskItemService)
        {
            this.taskItemService = taskItemService;
        }

        [HttpPost]
        public async ValueTask<ActionResult> PostTaskItemAsync(TaskItem taskItem)
        {
            try
            {
                TaskItem postTaskItem = await this.taskItemService.AddTaskItemAsync(taskItem);

                return Created(taskItem);
            }
            catch (TaskItemValidationException taskItemValidationException)
            {
                return BadRequest(taskItemValidationException.InnerException);
            }
            catch (TaskItemDependencyValidationException taskItemDependencyValidationException)
             when (taskItemDependencyValidationException.InnerException is AlreadyExistTaskItemException)
            {
                return Conflict(taskItemDependencyValidationException.InnerException);
            }
            catch (TaskItemDependencyValidationException taskItemDependencyValidationException)
            {
                return BadRequest(taskItemDependencyValidationException.InnerException);
            }
            catch (TaskItemDependencyException taskItemDependencyException)
            {
                return InternalServerError(taskItemDependencyException.InnerException);
            }
            catch (TaskItemServiceException taskItemServiceException)
            {
                return InternalServerError(taskItemServiceException.InnerException);
            }
        }
    }
}