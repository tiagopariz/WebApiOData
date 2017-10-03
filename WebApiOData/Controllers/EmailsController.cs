using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using WebApiOData.Entities;

namespace WebApiOData.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using WebApiOData.Entities;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Email>("Emails");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmailsController : ODataController
    {
        private Context.Context db = new Context.Context();

        // GET: odata/Emails
        [EnableQuery]
        public IQueryable<Email> GetEmails()
        {
            return db.Emails;
        }

        // GET: odata/Emails(5)
        [EnableQuery]
        public SingleResult<Email> GetEmail([FromODataUri] int key)
        {
            return SingleResult.Create(db.Emails.Where(email => email.EmailId == key));
        }

        // PUT: odata/Emails(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Email> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Email email = db.Emails.Find(key);
            if (email == null)
            {
                return NotFound();
            }

            patch.Put(email);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(email);
        }

        // POST: odata/Emails
        public IHttpActionResult Post(Email email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Emails.Add(email);
            db.SaveChanges();

            return Created(email);
        }

        // PATCH: odata/Emails(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Email> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Email email = db.Emails.Find(key);
            if (email == null)
            {
                return NotFound();
            }

            patch.Patch(email);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(email);
        }

        // DELETE: odata/Emails(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Email email = db.Emails.Find(key);
            if (email == null)
            {
                return NotFound();
            }

            db.Emails.Remove(email);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmailExists(int key)
        {
            return db.Emails.Count(e => e.EmailId == key) > 0;
        }
    }
}