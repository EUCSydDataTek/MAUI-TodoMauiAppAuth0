using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoApiAuth0.Data;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TodoDbContext>(opt =>
   opt.UseSqlite(builder.Configuration["ConnectionStrings:TodoDBConnectionString"]));

builder.Services.AddAuthentication().AddJwtBearer();

//builder.Services.AddAuthorization();  // Only use default policy

builder.Services.AddAuthorizationBuilder()
  .AddPolicy("ReadPolicy", p => p.RequireAuthenticatedUser().RequireClaim("permissions", "todo:read"))
  .AddPolicy("WritePolicy", p => p.RequireAuthenticatedUser().RequireClaim("permissions", "todo:write"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


#region GET: /
app.MapGet("/", () => "Hello World!");
#endregion

#region GET: /todoitems
app.MapGet("/todoitems", async Task<Ok<List<TodoItem>>> (TodoDbContext context) =>
{
    return TypedResults.Ok(await context.TodoItems.Where(t => t.Completed == false).ToListAsync());
})
.RequireAuthorization("ReadPolicy");
#endregion

#region GET: /todoitems/all
app.MapGet("/todoitems/all", async Task<Ok<List<TodoItem>>> (TodoDbContext context) =>
{
    return TypedResults.Ok(await context.TodoItems.ToListAsync());
})
.RequireAuthorization("ReadPolicy");
#endregion

#region GET: /todoitems/1
app.MapGet("/todoitems/{todoitemId:int}", async (TodoDbContext context, int todoitemId) =>
    await context.TodoItems.FirstOrDefaultAsync(t => t.Id == todoitemId) is TodoItem todoItem
        ? Results.Ok(todoItem)
        : Results.NotFound())
.RequireAuthorization("ReadPolicy");
#endregion

#region POST: /todoitems
app.MapPost("/todoitems", async (TodoDbContext context, TodoItem todoItem) =>
{
    todoItem.CreatedTime = DateTime.Now.ToUniversalTime();
    _ = context.TodoItems.Add(todoItem);
    _ = await context.SaveChangesAsync();
    return Results.Created($"/todoitems/{todoItem.Id}", todoItem);
})
.RequireAuthorization("WritePolicy");
#endregion

#region PUT: /todoitems/2
app.MapPut("/todoitems/{id:int}", async (int id, TodoItem inputTodoItem, TodoDbContext context) =>
{
    var todoItem = await context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
    if (todoItem is null) return Results.NotFound();
    todoItem.Description = inputTodoItem.Description;
    todoItem.Priority = inputTodoItem.Priority;
    todoItem.Completed = inputTodoItem.Completed;
    _ = await context.SaveChangesAsync();
    return Results.NoContent();
}).RequireAuthorization("WritePolicy");
#endregion

#region DELETE: /todoitems/5
app.MapDelete("/todoitems/{id:int}", async (int id, TodoDbContext context) =>
{
    if (await context.TodoItems.FirstOrDefaultAsync(t => t.Id == id) is TodoItem todoItem)
    {
        context.Remove(todoItem);
        _ = await context.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
})
.RequireAuthorization("WritePolicy");
#endregion

#region Recreate & migrate the database on each run for demo purposes
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var dbContext = services.GetRequiredService<TodoDbContext>();
//    dbContext.Database.EnsureDeleted();
//    dbContext.Database.Migrate();
//}
#endregion

app.Run();
