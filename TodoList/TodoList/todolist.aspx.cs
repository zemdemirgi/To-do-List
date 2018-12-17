using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;

namespace TodoList
{
    public partial class todolist : System.Web.UI.Page
    {
        
        SqlConnection baglanti = new SqlConnection("Data Source=GM-GIZEMDEMIR;Initial Catalog=todo-LİST;Integrated Security=True");
        SqlConnection baglanti2 = new SqlConnection("Data Source=GM-GIZEMDEMIR;Initial Catalog=todo-LİST;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {  
            liste();

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    foreach (Control control in cell.Controls)
                    {

                        ImageButton button = control as ImageButton;
                        if (button != null && button.CommandName == "X")
                            button.OnClientClick = "if (!confirm('Silmek istediğinize emin misiniz?')) return; ";

                    }

                }
            }
        }

        protected void Silme(object sender, GridViewDeleteEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            //sil(e.RowIndex);
        }
        protected void add_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

          
                ekleme();

                liste();
       
        }
        protected void add_ServerClick2(object sender, EventArgs e)
        {
        }


        void listele() //ekstra fonksiyon kullanmadım.
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                int i = 1;
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "select *from TodoTable";
                SqlDataAdapter adpr = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpr.Fill(ds, "TodoTable");
                //todoList.InnerHtml = " ";

                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    todoList.InnerHtml += i + ". " + row["Hedef"].ToString() + " " + "(" + row["time"].ToString() + ")" + "<br />";
                //    i++;
                //}

                baglanti.Close();

            }

        }

        void liste()
        {
            SqlConnection con = new SqlConnection("Data Source=GM-GIZEMDEMIR;Initial Catalog=todo-LİST;Integrated Security=True");
            SqlDataAdapter da = new SqlDataAdapter("select *from  TodoTable ", con);
            DataSet ds = new DataSet();
            DataSet db = new DataSet();
            try
            {
                da.Fill(ds, "TodoTable");
                da.Fill(db);
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
            finally
            {
                ds.Dispose();
                da.Dispose();
                con.Dispose();
            }
        }

        void sil(int id)
        {
            //Guid x = (Guid)GridView1.DataKeys[e.RowIndex].Values[0];

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = baglanti;
                cmd.CommandText = "DELETE FROM TodoTable WHERE  id=@numara";
                cmd.Parameters.AddWithValue("@numara", id);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                baglanti.Close();
                tarih(id);
                liste();
            }



        }

        void tarih(int id)
        {
            //Hedef silindiğinde ,bitiş tarihi atıyor.
            String BTarih = DateTime.Now.ToString();
            if (baglanti2.State == ConnectionState.Closed)
            {

                baglanti2.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti2;
                //cmd2.CommandText = "UPDATE TodoTableAll SET BTarih='" + BTarih + "' WHERE id = @numara2 ";
                cmd2.CommandText = "UPDATE TodoTableAll SET BTarih='" + BTarih + "'  WHERE  id='" + id + "'";
                //cmd2.Parameters.AddWithValue("@numara2", GridView1.DataKeys[e.RowIndex].Values["id"].ToString());
                //cmd2.Parameters.AddWithValue("@x", GridView1.Rows[e.RowIndex].Cells[0].Text);
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();

                baglanti2.Close();

            }
        }
        void ekleme()
        {
            if (baglanti.State == ConnectionState.Closed)
            {   
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti;
                string k = todoName.Value.Replace("'", "''");
                cmd.CommandText = "INSERT INTO TodoTable(Hedef,time) VALUES ('" + todoName.Value.Replace("'", "''") + "','" + todoTime.Value + "') ";
                cmd.ExecuteNonQuery();


                cmd2.CommandText = "SELECT id FROM TodoTable WHERE Hedef='" + k + "'";

                
                int e=int.Parse(cmd2.ExecuteScalar().ToString());
               
                cmd2.Dispose();
                cmd.Dispose();
                baglanti.Close();

                eklemeALL(e);
                
            }
        }

        void eklemeALL(int e)
        {
            String Tarih = DateTime.Now.ToString();

            if (baglanti.State == ConnectionState.Closed)
            {
                
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "INSERT INTO TodoTableAll(Hedef,time,Tarih,id) VALUES ('" + todoName.Value.Replace("'", "''") + "','" + todoTime.Value + "','" + Tarih + "','"+e+"')";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                baglanti.Close();
            }
        }
      
        protected void btnChange_Click(object sender, EventArgs e)
        {
            Response.Write("Button Clicked");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                sil(int.Parse(e.CommandArgument.ToString()));
            }
        }



        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            GridView1.PageIndex = e.NewPageIndex;
            liste();
          

        }


    }
}