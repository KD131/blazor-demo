using System.Net;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;
using Model.Entities;
using Shared.Services;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController(IPostService postService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        var posts = await postService.GetPostsAsync();
        return Ok(posts);
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Post>> GetPostById(int id)
    {
        try
        {
            var post = await postService.GetPostByIdAsync(id);
            return Ok(post);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound();
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Post>> CreatePost(PostDto post)
    {
        var createdPost = await postService.CreatePostAsync(post);
        return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdatePost(int id, PostDto post)
    {
        if (id != post.Id)
        {
            return BadRequest();
        }
        try
        {
            await postService.UpdatePostAsync(post);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound();
        }
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePost(int id)
    {
        try
        {
            await postService.DeletePostAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound();
        }
    }
}