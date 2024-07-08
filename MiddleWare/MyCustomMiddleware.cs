namespace WebApi.MiddleWare
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public MyCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Prima che la richiesta venga processata, cotnrolli di autenticazione per esempio
            Console.WriteLine("Prima della richiesta");

            // Passa la richiesta al prossimo middleware nella pipeline
            await _next(context);

            // Dopo che la risposta è stata generata
            Console.WriteLine("Dopo la risposta");
        }
    }
}
