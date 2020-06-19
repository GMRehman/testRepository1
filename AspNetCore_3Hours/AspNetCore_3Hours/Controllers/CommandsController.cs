using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore_3Hours.Data;
using AspNetCore_3Hours.DTOs;
using AspNetCore_3Hours.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_3Hours.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        //private readonly ICommanderRepo _repository = new ICommanderRepo();
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));//ok for http result 200 success
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult <CommandReadDTO> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDTO>(commandItem));//ok for http result 200 success
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO commandCreateDTO)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDTO);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDTO = _mapper.Map<CommandReadDTO>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDTO.Id}, commandReadDTO);
            //return Ok(commandReadDTO);
        }


        [HttpPut("{id}")]
        public ActionResult<CommandReadDTO> UpdateCommand(int id, CommandUpdateDTO commandUpdateDTO)
        {
            var commandModelIFromRepo = _repository.GetCommandById(id);
            if (commandModelIFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDTO, commandModelIFromRepo);

            _repository.UpdateCommand(commandModelIFromRepo);

            _repository.SaveChanges();

            return NoContent();
            //return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDTO.Id }, commandReadDTO);
            //return Ok(commandReadDTO);
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateCommand(int id, JsonPatchDocument<CommandUpdateDTO> patchDoc)
        {
            var commandModelIFromRepo = _repository.GetCommandById(id);
            if (commandModelIFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDTO>(commandModelIFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelIFromRepo);

            _repository.UpdateCommand(commandModelIFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelIFromRepo = _repository.GetCommandById(id);
            if (commandModelIFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandModelIFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
