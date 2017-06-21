﻿using LinqToExcel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueryGenerator
{
    /// <summary>
    /// Query Generator Form
    /// </summary>
    public partial class QueryGenerator : Form
    {
        #region Private Lists
        readonly private List<Person> listOPepoles;
        readonly private HashSet<string> hashSetOfCities;
        readonly private HashSet<string> hashSetCurrentOccupation;
        readonly private HashSet<string> hashSetExternalContact;
        readonly private HashSet<string> hashSetUseAlcohol;
        readonly private HashSet<string> hashSetUseDrug;
        readonly private HashSet<string> hashSetReligion;
        readonly private HashSet<string> hashSetlistOfCriminalRecord;
        readonly private HashSet<string> hashSetlistOfInstitutions;
        readonly private HashSet<string> hashSetlistOfPostitutions;
        readonly private HashSet<string> hashSetlistOfShelter;
        readonly private IEnumerable<Person> mainQuery;
        private IEnumerable<Person> listAfterQuery;
        readonly private List<Panel> allPanels;
        readonly private List<CheckBox> allCheckBoxes;
        readonly private List<ComboBox> allComboBoxes;

        int sum;
        #endregion

        #region Private Query Parameters
        private string genderValue;
        private int fromAge, toAge;
        private string city;
        private string drug;
        private bool FindAppearance;
        private DateTime appStartDate, appEndDate;
        private int fromAppearance, toAppearance;
        private string alcohol;
        private string religion;
        private string occupation;
        private string criminalRecord;
        private string externalContact;
        private string postitution;
        private string institution;
        private string shelder;
        #endregion
        public QueryGenerator(List<Person> listOfPepoles, HashSet<string> hashSetOfcities, HashSet<string> hashSetCurrentoccupation, HashSet<string> hashSetExternalcontact
            , HashSet<string> hashSetUsealcohol, HashSet<string> hashSetUsedrug, HashSet<string> hashSetreligion,
            HashSet<string> hashSetlistOfCriminalrecord, HashSet<string> hashSetlistOfinstitutions,
            HashSet<string> hashSetlistOfpostitutions, HashSet<string> hashSetlistOfshelter)
        {
            InitializeComponent();
            try
            {
                SetFormSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show("תקלה בטעינת התוכנה, פתח מחדש", "החוט המשולש", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            listOPepoles = listOfPepoles;
            hashSetOfCities = hashSetOfcities;
            hashSetCurrentOccupation = hashSetCurrentoccupation;
            hashSetExternalContact = hashSetExternalcontact;
            hashSetUseAlcohol = hashSetUsealcohol;
            hashSetUseDrug = hashSetUsedrug;
            hashSetReligion = hashSetreligion;
            hashSetlistOfCriminalRecord = hashSetlistOfCriminalrecord;
            hashSetlistOfInstitutions = hashSetlistOfinstitutions;
            hashSetlistOfPostitutions = hashSetlistOfpostitutions;
            hashSetlistOfShelter = hashSetlistOfshelter;

            mainQuery = from p in listOPepoles select p;
            listAfterQuery = mainQuery;
            allPanels = new List<Panel> {
                panelAge,panelGender, panelAlcohol, panelCity, panelCriminalRecord, panelDate, panelDrug, panelExternalContact, panelReligion,panelOccupation,
                panelpostitution,panelshelder,panelinstitution };
            allCheckBoxes = new List<CheckBox>{
                cb_age, cb_gender,cb_alcohol,cb_city,cb_criminalRecord,cb_attendance, cb_useDrug, cb_externalContact,cb_religion,
                cb_occupation,cb_postitution,cb_shelder,cb_institution};
            allComboBoxes = new List<ComboBox>{
                externalContactCB, cityCB, criminalRecordCB, occupationCB, religionCB, drugsCB, AlcoholCB,postitutionCB,shelderCB,institutionCB};
        }

        #region CheckBox CheckedChanged functionality
        private void cb_gender_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_gender.Checked)
            {
                unCheckedAllBut(cb_gender);
                panelGender.BringToFront();
                cb_gender.Checked = true;
                disableVisabilityPanels();
                panelGender.Visible = true;
            }
            else
            {
                panelGender.Visible = false;
            }
        }
        private void cb_age_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_age.Checked)
            {
                unCheckedAllBut(cb_age);
                panelAge.BringToFront();
                cb_age.Checked = true;
                disableVisabilityPanels();
                panelAge.Visible = true;
            }
            else
            {
                panelAge.Visible = false;
            }
        }
        private void cb_alcohol_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_alcohol.Checked)
            {
                unCheckedAllBut(cb_alcohol);
                panelAlcohol.BringToFront();
                disableVisabilityPanels();
                AlcoholCB.Items.Clear();
                AlcoholCB.Items.AddRange(hashSetUseAlcohol.ToArray());
                panelAlcohol.Visible = true;
            }
            else
            {
                panelAlcohol.Visible = false;
            }
        }
        private void cb_useDrug_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_useDrug.Checked)
            {
                unCheckedAllBut(cb_useDrug);
                panelDrug.BringToFront();
                disableVisabilityPanels();
                drugsCB.Items.Clear();
                drugsCB.Items.AddRange(hashSetUseDrug.ToArray());
                panelDrug.Visible = true;
            }
            else
            {
                panelDrug.Visible = false;
            }

        }
        private void cb_religion_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_religion.Checked)
            {
                unCheckedAllBut(cb_religion);
                panelReligion.BringToFront();
                disableVisabilityPanels();
                religionCB.Items.Clear();
                religionCB.Items.AddRange(hashSetReligion.ToArray());
                panelReligion.Visible = true;
            }
            else
            {
                panelReligion.Visible = false;
            }

        }
        private void cb_occupation_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_occupation.Checked)
            {
                unCheckedAllBut(cb_occupation);
                panelOccupation.BringToFront();
                disableVisabilityPanels();
                occupationCB.Items.Clear();
                occupationCB.Items.AddRange(hashSetCurrentOccupation.ToArray());
                panelOccupation.Visible = true;
            }
            else
            {
                panelOccupation.Visible = false;
            }

        }
        private void cb_criminalRecord_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_criminalRecord.Checked)
            {
                unCheckedAllBut(cb_criminalRecord);
                panelCriminalRecord.BringToFront();
                disableVisabilityPanels();
                criminalRecordCB.Items.Clear();
                criminalRecordCB.Items.AddRange(hashSetlistOfCriminalRecord.ToArray());
                panelCriminalRecord.Visible = true;
            }
            else
            {
                panelCriminalRecord.Visible = false;
            }
        }
        private void cb_city_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_city.Checked)
            {
                unCheckedAllBut(cb_city);
                disableVisabilityPanels();
                panelCity.BringToFront();
                cityCB.Items.Clear();
                cityCB.Items.AddRange(hashSetOfCities.ToArray());
                panelCity.Visible = true;
            }
            else
            {
                panelCity.Visible = false;
            }
        }
        private void cb_externalContact_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_externalContact.Checked)
            {
                unCheckedAllBut(cb_externalContact);
                panelExternalContact.BringToFront();
                disableVisabilityPanels();
                externalContactCB.Items.Clear();
                externalContactCB.Items.AddRange(hashSetExternalContact.ToArray());
                panelExternalContact.Visible = true;
            }
            else
            {
                panelExternalContact.Visible = false;
            }
        }
        private void cb_appearance_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_attendance.Checked)
            {
                unCheckedAllBut(cb_attendance);
                disableVisabilityPanels();
                panelDate.Visible = true;
            }
            else
            {
                panelDate.Visible = false;
            }
        }
        private void cb_postitution_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_postitution.Checked)
            {
                unCheckedAllBut(cb_postitution);
                disableVisabilityPanels();
                postitutionCB.Items.Clear();
                postitutionCB.Items.AddRange(hashSetlistOfPostitutions.ToArray());
                panelpostitution.Visible = true;
            }
            else
            {
                panelpostitution.Visible = false;
            }
        }

        private void cb_shelder_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_shelder.Checked)
            {
                unCheckedAllBut(cb_shelder);
                disableVisabilityPanels();
                shelderCB.Items.Clear();
                shelderCB.Items.AddRange(hashSetlistOfShelter.ToArray());
                panelshelder.Visible = true;
            }
            else
            {
                panelshelder.Visible = false;
            }
        }
        private void cb_institution_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_institution.Checked)
            {
                unCheckedAllBut(cb_institution);
                disableVisabilityPanels();
                institutionCB.Items.Clear();
                institutionCB.Items.AddRange(hashSetlistOfInstitutions.ToArray());
                panelinstitution.Visible = true;
            }
            else
            {
                panelinstitution.Visible = false;
            }
        }
        #endregion

        #region Buttons Click functionality
        private void createListFromQuery_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("מס'", typeof(string));
            dt.Columns.Add("שם פרטי", typeof(string));
            dt.Columns.Add("שם משפחה", typeof(string));
            dt.Columns.Add("גיל", typeof(int));
            dt.Columns.Add("מין", typeof(string));
            dt.Columns.Add("עיר", typeof(string));
            int numOfColumns = 0;

            if (genderValue != null)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.gender == genderValue
                                 orderby person.firstName ascending
                                 select person;
            }
            if (fromAge != 0 && toAge != 0)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.age >= fromAge && person.age <= toAge
                                 orderby person.firstName ascending
                                 select person;
            }
            if (city != null)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.city == city
                                 orderby person.firstName ascending
                                 select person;
            }
            if (drug != null)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.UseDrug == drug
                                 orderby person.firstName ascending
                                 select person;
                dt.Columns.Add("שימוש בסמים", typeof(string));
                numOfColumns++;
            }
            if (alcohol != null)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.UseAlcohol == alcohol
                                 orderby person.firstName ascending
                                 select person;
                dt.Columns.Add("שימוש באלכוהול", typeof(string));
                numOfColumns++;
            }
            if (religion != null)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.Religion == religion
                                 orderby person.firstName ascending
                                 select person;
                dt.Columns.Add("רקע", typeof(string));
                numOfColumns++;
            }
            if (occupation != null)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.CurrentOccupation == occupation
                                 orderby person.firstName ascending
                                 select person;
                dt.Columns.Add("עיסוק נוכחי", typeof(string));
                numOfColumns++;
            }
            if (criminalRecord != null)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.CriminalRecord == criminalRecord
                                 orderby person.firstName ascending
                                 select person;
                dt.Columns.Add("רישום פלילי", typeof(string));
                numOfColumns++;
            }
            if (externalContact != null)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.ExternalContact == externalContact
                                 orderby person.firstName ascending
                                 select person;
                dt.Columns.Add("קשר עם גורם נוסף", typeof(string));
                numOfColumns++;
            }
            if (institution != null)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.NumOfInstitutions == institution
                                 orderby person.firstName ascending
                                 select person;
                dt.Columns.Add("מס מסדות", typeof(string));
                numOfColumns++;
            }
            if (postitution != null)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.PostitutionSequence == postitution
                                 orderby person.firstName ascending
                                 select person;
                dt.Columns.Add("רצף זנות", typeof(string));
                numOfColumns++;
            }
            if (shelder != null)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.ShelterSequence == shelder
                                 orderby person.firstName ascending
                                 select person;
                dt.Columns.Add("רצף קורת גג", typeof(string));
                numOfColumns++;
            }

            if (FindAppearance)
            {
                listAfterQuery = from person in listAfterQuery
                                 where person.numOfArrivalesInRange(appStartDate, appEndDate) >= fromAppearance && person.numOfArrivalesInRange(appStartDate, appEndDate) <= toAppearance
                                 orderby person.numOfArrivalesInRange(appStartDate, appEndDate) descending
                                 select person;

                dt.Columns.Add("מס' הגעות", typeof(string));
                numOfColumns++;
            }


            sum = 0;
            int temp = numOfColumns;
            foreach (Person p in listAfterQuery)
            {
                numOfColumns = temp;
                DataRow r = dt.Rows.Add(sum + 1, p.firstName, p.lastName, p.age, p.gender, p.city);
                if (FindAppearance)
                {
                    r.SetField(numOfColumns + 5, p.numOfArrivalesInRange(appStartDate, appEndDate));
                    numOfColumns--;
                }
                if (shelder != null)
                {
                    r.SetField(numOfColumns + 5, p.ShelterSequence);
                    numOfColumns--;
                }
                if (postitution != null)
                {
                    r.SetField(numOfColumns + 5, p.PostitutionSequence);
                    numOfColumns--;
                }
                if (institution != null)
                {
                    r.SetField(numOfColumns + 5, p.NumOfInstitutions);
                    numOfColumns--;
                }  
                if (externalContact != null)
                {
                    r.SetField(numOfColumns + 5, p.ExternalContact);
                    numOfColumns--;
                }
                if (criminalRecord != null)
                {
                    r.SetField(numOfColumns + 5, p.CriminalRecord);
                    numOfColumns--;
                }
                if (occupation != null)
                {
                    r.SetField(numOfColumns + 5, p.CurrentOccupation);
                    numOfColumns--;
                }
                if (religion != null)
                {
                    r.SetField(numOfColumns + 5, p.Religion);
                    numOfColumns--;
                }
                if (alcohol != null)
                {
                    r.SetField(numOfColumns + 5, p.UseAlcohol);
                    numOfColumns--;
                }
                if (drug != null)
                {
                    r.SetField(numOfColumns + 5, p.UseDrug);
                }
                sum++;
            }
            SumLabel.Text = sum.ToString();
            dataListGrid.DataSource = dt;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            //Clear Query
            listAfterQuery = mainQuery;
            //ClearGenderPanel
            genderValue = null;
            if (Radio_men.Checked)
            {
                Radio_men.Checked = !Radio_men.Checked;
            }
            if (Radio_woman.Checked)
            {
                Radio_woman.Checked = !Radio_woman.Checked;
            }
            if (Radio_unknowGender.Checked)
            {
                Radio_unknowGender.Checked = !Radio_unknowGender.Checked;
            }
            //ClearAgePanel
            fromAge = 0;
            ageFromNumeric.Value = 15;
            toAge = 25;
            ageToNumeric.Value = 24;
            //ClearCityPanel
            if (city != null)
            {
                cityCB.SelectedIndex = 0;
                city = null;
            }
            //ClearAlcohol
            if (alcohol != null)
            {
                AlcoholCB.SelectedIndex = 0;
                alcohol = null;
            }
            //clear drug
            if (drug != null)
            {
                drugsCB.SelectedIndex = 0;
                drug = null;
            }
            //ClearReligion
            if (religion != null)
            {
                religionCB.SelectedIndex = 0;
                religion = null;
            }
            //ClearOccupation
            if (occupation != null)
            {
                occupationCB.SelectedIndex = 0;
                occupation = null;
            }
            //ClearCrimialRecord
            if (criminalRecord != null)
            {
                criminalRecordCB.SelectedIndex = 0;
                criminalRecord = null;
            }
            if (externalContact != null)
            {
                externalContactCB.SelectedIndex = 0;
                externalContact = null;
            }
            if (institution != null)
            {
                institutionCB.SelectedIndex = 0;
                institution = null;
            }
            if (postitution != null)
            {
                postitutionCB.SelectedIndex = 0;
                postitution = null;
            }
            if (shelder != null)
            {
                shelderCB.SelectedIndex = 0;
                shelder = null;
            }


            //clear Apperance
            dateStart.Refresh();
            dateEnd.Refresh();
            appStartDate = dateStart.Value.Date;
            appEndDate = dateEnd.Value.Date;
            fromAppearance = 0;
            toAppearance = 0;
            FindAppearance = false;
            //ClearQueryPanel
            listAfterQuery = mainQuery;
            QueryListBox.Items.Clear();
            //ClearTable
            dataListGrid.DataSource = null;

            SumLabel.Text = "0";
            disableVisabilityPanels();
            unCheckedAllBut(null);
        }
        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (QueryListBox.SelectedIndex >= 0)
            {
                listAfterQuery = mainQuery;
                string queryToRemove = QueryListBox.SelectedItem.ToString();
                if (queryToRemove.Contains("מין"))
                {
                    genderValue = null;
                    if (Radio_men.Checked)
                    {
                        Radio_men.Checked = !Radio_men.Checked;
                    }
                    if (Radio_woman.Checked)
                    {
                        Radio_woman.Checked = !Radio_woman.Checked;
                    }
                    if (Radio_unknowGender.Checked)
                    {
                        Radio_unknowGender.Checked = !Radio_unknowGender.Checked;
                    }
                }
                else if (queryToRemove.Contains("גיל"))
                {
                    fromAge = 15;
                    ageFromNumeric.Value = 15;
                    toAge = 24;
                    ageToNumeric.Value = 24;
                }
                else if (queryToRemove.Contains("אלכוהול"))
                {
                    AlcoholCB.SelectedIndex = 0;
                    alcohol = null;
                }
                else if (queryToRemove.Contains("סמים"))
                {
                    drugsCB.SelectedIndex = 0;
                    drug = null;
                }
                else if (queryToRemove.Contains("רקע"))
                {
                    religionCB.SelectedIndex = 0;
                    religion = null;
                }
                else if (queryToRemove.Contains("עיסוק"))
                {
                    occupationCB.SelectedIndex = 0;
                    occupation = null;
                }
                else if (queryToRemove.Contains("רישום"))
                {
                    criminalRecordCB.SelectedIndex = 0;
                    criminalRecord = null;
                }
                else if (queryToRemove.Contains("נוסף"))
                {
                    externalContactCB.SelectedIndex = 0;
                    externalContact = null;
                }
                else if (queryToRemove.Contains("זנות"))
                {
                    postitutionCB.SelectedIndex = 0;
                    postitution = null;
                }
                else if (queryToRemove.Contains("קורת גג"))
                {
                    shelderCB.SelectedIndex = 0;
                    shelder = null;
                }
                else if (queryToRemove.Contains("מסדות"))
                {
                    institutionCB.SelectedIndex = 0;
                    institution = null;
                }
                else if (queryToRemove.Contains("נוכחות"))
                {
                    dateStart.Refresh();
                    dateEnd.Refresh();
                    appStartDate = dateStart.Value.Date;
                    appEndDate = dateEnd.Value.Date;
                    fromAppearance = 0;
                    toAppearance = 0;
                    FindAppearance = false;
                }
                else if (queryToRemove.Contains("עיר"))
                {
                    cityCB.SelectedIndex = 0;
                    city = null;
                }
                QueryListBox.Items.RemoveAt(QueryListBox.SelectedIndex);
            }
        }
        private void generateChart_Click(object sender, EventArgs e)
        {
            if (cbIgnoreEmpty.SelectedItem != null)
            {
                generateChartAndIgnore(cbIgnoreEmpty.SelectedIndex);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Un Check all Check Box other than cb
        /// To UnCheck all cb = null
        /// </summary>
        /// <param name="cb"></param>
        private static List<Person> fixList(IEnumerable<Person> list, string query)
        {
            List<Person> temp = new List<Person>();
            foreach (var n in list)
            {
                if (query == "מין")
                {
                    if (n.gender == "זכר" || n.gender == "נקבה")
                    {
                        temp.Add(n);
                    }
                }
                else if (query == "עיר")
                {
                    if (n.city != "")
                    {
                        temp.Add(n);
                    }
                }
                else if (query == "גיל")
                {
                    if (n.age >= 14 && n.age <= 25)
                    {
                        temp.Add(n);
                    }
                }
                else if (query == "רקע")
                {
                    if (n.Religion != "")
                    {
                        temp.Add(n);
                    }
                }
                else if (query == "שימוש בסמים")
                {
                    if (n.UseDrug != "")
                    {
                        temp.Add(n);
                    }
                }
                else if (query == "שימוש באלכוהול")
                {
                    if (n.UseAlcohol != "")
                    {
                        temp.Add(n);
                    }
                }

            }
            return temp;
        }
        private void generateChartAndIgnore(int ignore)
        {
            if (chartList.SelectedItem == null || cb_chartType.SelectedItem == null)
            {
                MessageBox.Show("יש לבחור שאילתה וסוג גרף", "החוט המשולש", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string query = chartList.SelectedItem.ToString();
            string graphType = cb_chartType.SelectedItem.ToString();
            string[] titles;
            string[] genderArray = { "זכר", "נקבה", "אחר" };
            string[] ageArray = { "15", "16", "17", "18", "19", "20", "21", "22", "23", "24" };
            HashSet<string> temp;
            switch (query)
            {
                case "מין":
                    titles = genderArray;
                    break;
                case "עיר":
                    temp = hashSetOfCities;
                    if (ignore == 0)
                    {
                        temp.Remove("");
                    }
                    titles = temp.ToArray();
                    break;
                case "רקע":
                    temp = hashSetReligion;
                    if (ignore == 0)
                    {
                        temp.Remove("");
                    }
                    titles = temp.ToArray();
                    break;
                case "גיל":
                    titles = ageArray;
                    break;
                case "שימוש באלכוהול":
                    temp = hashSetUseAlcohol;
                    if (ignore == 0)
                    {
                        temp.Remove("");
                    }
                    titles = temp.ToArray();
                    break;
                case "שימוש בסמים":
                    temp = hashSetUseDrug;
                    if (ignore == 0)
                    {
                        temp.Remove("");
                    }
                    titles = temp.ToArray();                   
                    break;
                case "עיסוק נוכחי":
                    temp = hashSetCurrentOccupation;
                    if (ignore == 0)
                    {
                        temp.Remove("");
                    }
                    titles = temp.ToArray();
                    break;
                case "רישום פלילי":
                    titles = hashSetlistOfCriminalRecord.ToArray();
                    break;
                case "רצף זנות":
                    titles = hashSetlistOfPostitutions.ToArray();
                    break;
                case "מס' מוסדות":
                    titles = hashSetlistOfInstitutions.ToArray();
                    break;
                case "רצף קורת גג":
                    titles = hashSetlistOfShelter.ToArray();
                    break;
                case "קשר עם גורם נוסף":
                    titles = hashSetExternalContact.ToArray();
                    break;
                default:
                    titles = null;
                    break;
            }
            int[] counter = new int[titles.Length];
            IEnumerable<Person> p;
            if (ignore == 0)
            {
                p = fixList(listAfterQuery, query);
            }
            else
            {
                p = listAfterQuery;
            }
            foreach (var person in p)
            {
                for (int i = 0; i < titles.Length; i++)
                {
                    if (query == "מין")
                    {
                        if (person.gender != "זכר" && person.gender != "נקבה")
                        {
                            counter[2]++;
                        }
                        else if (person.gender == titles[i])
                        {
                            counter[i]++;
                        }
                    }
                    if (query == "עיר")
                    {
                        if (person.city == titles[i])
                        {
                            counter[i]++;
                        }
                    }
                    if (query == "רקע")
                    {
                        if (person.Religion == titles[i])
                        {
                            counter[i]++;
                        }
                    }
                    if (query == "גיל")
                    {
                        if (person.age == int.Parse(titles[i]))
                        {
                            counter[i]++;
                        }
                    }
                    if (query == "שימוש באלכוהול")
                    {
                        if (person.UseAlcohol == titles[i])
                        {
                            counter[i]++;
                        }
                    }
                    if (query == "שימוש בסמים")
                    {
                        if (person.UseDrug == titles[i])
                        {
                            counter[i]++;
                        }
                    }
                    if (query == "רישום פלילי")
                    {
                        if (person.CriminalRecord == titles[i])
                        {
                            counter[i]++;
                        }
                    }
                    if (query == "קשר עם גורם נוסף")
                    {
                        if (person.ExternalContact == titles[i])
                        {
                            counter[i]++;
                        }
                    }
                    if (query == "מס' מוסדות")
                    {
                        if (person.NumOfInstitutions == titles[i])
                        {
                            counter[i]++;
                        }
                    }
                    if (query == "רצף זנות")
                    {
                        if (person.PostitutionSequence == titles[i])
                        {
                            counter[i]++;
                        }
                    }
                    if (query == "רצף קורת גג")
                    {
                        if (person.ShelterSequence == titles[i])
                        {
                            counter[i]++;
                        }
                    }
                    if (query == "עיסוק נוכחי")
                    {
                        if (person.CurrentOccupation == titles[i])
                        {
                            counter[i]++;
                        }
                    }


                }
            }
            QueryGraph g = new QueryGraph(titles, counter, graphType);
            g.Visible = true;
        }
        private void addQueryBtn_Click(object sender, EventArgs e)
        {
            if (panelGender.Visible)
            {
                if (Radio_men.Checked)
                {
                    genderValue = "זכר";
                }
                else if (Radio_woman.Checked)
                {
                    genderValue = "נקבה";
                }
                else
                {
                    genderValue = "";
                }
                QueryListBox.Items.Add("לפי מין: " + genderValue);
            }
            if (panelAge.Visible)
            {
                fromAge = (int)ageFromNumeric.Value;
                toAge = (int)ageToNumeric.Value;
                QueryListBox.Items.Add("טווח גילאים: " + fromAge + " - " + toAge);
            }
            if (panelCity.Visible && cityCB.SelectedItem != null)
            {
                city = cityCB.SelectedItem.ToString();
                QueryListBox.Items.Add("לפי עיר: " + city);
            }
            if (panelAlcohol.Visible && AlcoholCB.SelectedItem != null)
            {
                alcohol = AlcoholCB.SelectedItem.ToString();
                QueryListBox.Items.Add("שימוש באלכוהול: " + alcohol);
            }
            if (panelDrug.Visible && drugsCB.SelectedItem != null)
            {
                drug = drugsCB.SelectedItem.ToString();
                QueryListBox.Items.Add("שימוש בסמים: " + drug);
            }
            if (panelReligion.Visible && religionCB.SelectedItem != null)
            {
                religion = religionCB.SelectedItem.ToString();
                QueryListBox.Items.Add("רקע: " + religion);
            }
            if (panelOccupation.Visible && occupationCB.SelectedItem != null)
            {
                occupation = occupationCB.SelectedItem.ToString();
                QueryListBox.Items.Add("עיסוק נוכחי: " + occupation);
            }
            if (panelCriminalRecord.Visible && criminalRecordCB.SelectedItem != null)
            {
                criminalRecord = criminalRecordCB.SelectedItem.ToString();
                QueryListBox.Items.Add("רישום פלילי: " + criminalRecord);
            }
            if (panelExternalContact.Visible && externalContactCB.SelectedItem != null)
            {
                externalContact = externalContactCB.SelectedItem.ToString();
                QueryListBox.Items.Add("קשר עם גורם נוסף: " + externalContact);
            }
            if (panelinstitution.Visible && institutionCB.SelectedItem != null)
            {
                institution = institutionCB.SelectedItem.ToString();
                QueryListBox.Items.Add("מס מסדות: " + institution);
            }
            if (panelpostitution.Visible && postitutionCB.SelectedItem != null)
            {
                postitution = postitutionCB.SelectedItem.ToString();
                QueryListBox.Items.Add("רצף זנות: " + postitution);
            }
            if (panelshelder.Visible && shelderCB.SelectedItem != null)
            {
                shelder = shelderCB.SelectedItem.ToString();
                QueryListBox.Items.Add("רצף קורת גג: " + shelder);
            }
            if (panelDate.Visible)
            {
                appStartDate = dateStart.Value.Date;
                appEndDate = dateEnd.Value.Date;
                fromAppearance = (int)dateFromNumeric.Value;
                toAppearance = (int)dateToNumeric.Value;
                FindAppearance = true;
                QueryListBox.Items.Add(string.Format("From: {0} To: {1} arrived {2}-{3} times", appStartDate.ToString("d"), appEndDate.ToString("d"), fromAppearance, toAppearance));
            }
        }
        private void unCheckedAllBut(CheckBox cb)
        {
            foreach (CheckBox box in allCheckBoxes)
            {
                if (box != cb)
                {
                    box.Checked = false;
                }
            }
        }
        private void QueryGenerator_FormClosing(object sender,FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            StringBuilder text = new StringBuilder("\n");
            foreach (var item in QueryListBox.Items)
            {
                text.Append(item.ToString() + "\n"); // /n to print each item on new line or you omit /n to print text on same line
            }
            ClsPrint _ClsPrint = new ClsPrint(dataListGrid,string.Format( "החוט המשולש -  {0} מספר תוצאות {1} \n לפי שאילתות:{2}", DateTime.Today.ToShortDateString() , sum, text));
            _ClsPrint.PrintForm();
        }

        private void disableVisabilityPanels()
        {
            foreach (Panel p in allPanels)
            {
                p.Visible = false;
                p.Location = new Point(0, label2.Height + (int)(this.Height * 0.06));
                p.Size = new Size((int)(this.Height * 0.2), (int)(this.Width * 0.3));
            }

            foreach (ComboBox box in allComboBoxes)
            {
                box.Size = new Size((int)(panelAge.Width), (int)(this.Height * 0.5));
                box.Location = new Point(panelExternalContact.Width / 2 - externalContactCB.Width / 2, label12.Height);
                box.Font = new Font("Arial", 9, FontStyle.Regular);
            }

            label4.Font = new Font("Arial", 12, FontStyle.Bold);
            label5.Font = new Font("Arial", 12, FontStyle.Bold);

            label12.Size = new Size(panelCity.Width, panelCity.Height / 2);
            label12.Location = new Point(panelCity.Width / 2 - label12.Width / 2, 0);
            label12.Font = new Font("Arial", 12, FontStyle.Bold);

            label7.Size = new Size(panelCity.Width, panelCity.Height / 2);
            label7.Location = new Point(panelCity.Width / 2 - label7.Width / 2, 0);
            label7.Font = new Font("Arial", 12, FontStyle.Bold);

            label13.Size = new Size(panelCity.Width, panelCity.Height / 2);
            label13.Location = new Point(panelCity.Width / 2 - label13.Width / 2, 0);
            label13.Font = new Font("Arial", 12, FontStyle.Bold);

            label10.Size = new Size(panelCity.Width, panelCity.Height / 2);
            label10.Location = new Point(panelCity.Width / 2 - label10.Width / 2, 0);
            label10.Font = new Font("Arial", 12, FontStyle.Bold);

            label6.Size = new Size(panelCity.Width, panelCity.Height / 2);
            label6.Location = new Point(panelCity.Width / 2 - label6.Width / 2, 0);
            label6.Font = new Font("Arial", 12, FontStyle.Bold);

            label11.Size = new Size(panelCity.Width, panelCity.Height / 2);
            label11.Location = new Point(panelCity.Width / 2 - label11.Width / 2, 0);
            label11.Font = new Font("Arial", 12, FontStyle.Bold);

            label14.Size = new Size(panelCity.Width, panelCity.Height / 2);
            label14.Location = new Point(panelCity.Width / 2 - label14.Width / 2, 0);
            label14.Font = new Font("Arial", 12, FontStyle.Bold);

            label16.Size = new Size(panelCity.Width, panelCity.Height / 2);
            label16.Location = new Point(panelCity.Width / 2 - label16.Width / 2, 0);
            label16.Font = new Font("Arial", 12, FontStyle.Bold);

            label17.Size = new Size(panelCity.Width, panelCity.Height / 2);
            label17.Location = new Point(panelCity.Width / 2 - label17.Width / 2, 0);
            label17.Font = new Font("Arial", 12, FontStyle.Bold);

            label18.Size = new Size(panelCity.Width, panelCity.Height / 2);
            label18.Location = new Point(panelCity.Width / 2 - label18.Width / 2, 0);
            label18.Font = new Font("Arial", 12, FontStyle.Bold);
        }
        private void SetFormSize()
        {
            Rectangle rect = Screen.FromHandle(this.Handle).WorkingArea;
            rect.Location = new Point(0, 0);
            this.MaximizedBounds = rect;
            this.WindowState = FormWindowState.Maximized;
            panelResults.Width = (int)(this.Width * 0.7);
            panelResults.Height = (int)(this.Height - panelGraph.Height);
            panelResults.Location = new Point((int)(this.Width * 0.02), 0);
            dataListGrid.Size = new Size(panelResults.Width - (int)(this.Width * 0.02), (int)(this.panelResults.Height - label1.Height - (int)(this.Height * 0.04)));
            panelGraph.Location = new Point(panelResults.Width / 2 - panelGraph.Width / 2, this.Height - panelGraph.Height - (int)(this.Height * 0.02));
            panelChooseQuery.Width = (int)(this.Width * 0.3);
            panelChooseQuery.Height = (int)(label2.Height + panelQuery.Height + addQueryBtn.Height + (int)(this.Height * 0.02));
            panelChooseQuery.Location = new Point(panelResults.Width + (int)(this.Width * 0.02), 0);
            panelChooseQuery.Paint += delegate (object o, PaintEventArgs p)
            {
                p.Graphics.Clear(Color.FromArgb(255, 192, 128));
            };

            queryBox.Width = (int)(this.Width * 0.3);
            queryBox.Location = new Point(panelResults.Width + (int)(this.Width * 0.02), panelChooseQuery.Location.Y + panelChooseQuery.Height);
            queryBox.Height = (int)(this.Height * 0.3);
            QueryListBox.Size = new Size(queryBox.Width - (int)(queryBox.Width * 0.01), queryBox.Height - btnClear.Height);
            QueryListBox.Location = new Point(0, 0);
            remove.Location = new Point(QueryListBox.Width - remove.Width, QueryListBox.Height);
            btnClear.Location = new Point(0, QueryListBox.Height);
            label2.Location = new Point(queryBox.Width / 2 - label2.Width / 2, (int)(this.Height * 0.01));
            addQueryBtn.Location = new Point(queryBox.Width / 2 - addQueryBtn.Width / 2, panelQuery.Height + label2.Height + (int)(this.Height * 0.01));
            panelQuery.Location = new Point(panelChooseQuery.Width - panelQuery.Width, label2.Height);
            createListFromQuery.Location = new Point(queryBox.Location.X + (queryBox.Width / 2 - createListFromQuery.Width / 2), panelGraph.Location.Y);
        }
        #endregion
    }
}


