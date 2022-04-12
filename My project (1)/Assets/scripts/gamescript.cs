using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class gamescript : MonoBehaviour
{
    public QuestionList[] questions;
    public Text[] answersText;
    public Text qText;
    public Button[] answerBttns = new Button[3];
    public Sprite[] TFIcons = new Sprite[2];
    public Image TFIcon;
    public Text TFText; 
    List<object> qList;
    QuestionList crntQ;
    public GameObject Cnopochka;
    int randQ;

    public void OnClickPlay()
    {
        
        qList = new List<object>(questions);
        questionGenerate();
       Cnopochka.gameObject.SetActive(false);
        
    }
    void questionGenerate()
    {
        if (qList.Count > 0)
        {
            randQ = Random.Range(0, qList.Count);
            crntQ = qList[randQ] as QuestionList;
            qText.text = crntQ.question;
            qText.gameObject.SetActive(true);
            List<string> answers = new List<string>(crntQ.answers);

            for (int i = 0; i < crntQ.answers.Length; i++)
            {
                int rand = Random.Range(0, answers.Count);
                answersText[i].text = answers[rand];
                answers.RemoveAt(rand);
            }
            StartCoroutine(animBttns());
        }
        else
        {
            print("Поздравляем! Теперь Вы знаете строение скелета человека! :D");
        }
    }
    IEnumerator animBttns()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < answerBttns.Length; i++) answerBttns[i].interactable = false;
        int a = 0;

        while (a < answerBttns.Length)
        {
            if (!answerBttns[a].gameObject.activeSelf) answerBttns[a].gameObject.SetActive(true);
            else answerBttns[a].gameObject.GetComponent<Animator>().SetTrigger("In");
            a++;
            yield return new WaitForSeconds(1);
        }
        for (int i = 0; i < answerBttns.Length; i++) answerBttns[i].interactable = true;
        yield break;
    }
    IEnumerator trueOrFalse(bool check)
    {
        for (int i = 0; i < answerBttns.Length; i++) answerBttns[i].interactable = false;
        for (int i = 0; i < answerBttns.Length; i++) answerBttns[i].gameObject.GetComponent<Animator>().SetTrigger("Out");
        qText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if (!TFIcon.gameObject.activeSelf) TFIcon.gameObject.SetActive(true);
        else TFIcon.gameObject.GetComponent<Animator>().SetTrigger("In");
        if (check)
        {
            TFIcon.sprite = TFIcons[0];
            TFText.text = "Правильный ответ! :)";
            yield return new WaitForSeconds(1.5f);
            TFIcon.gameObject.GetComponent<Animator>().SetTrigger("Out");
            TFIcon.gameObject.SetActive(false);
            qList.RemoveAt(randQ);
            questionGenerate();
            yield break;
        }
        else
        {
            TFIcon.sprite = TFIcons[1];
            TFText.text = "Неправильный ответ! :(";
            yield return new WaitForSeconds(1.5f);
            TFIcon.gameObject.GetComponent<Animator>().SetTrigger("Out");
            TFIcon.gameObject.SetActive(false);
            questionGenerate();
            yield break;
        }
    }
    public void answersBttns(int index)
    {
        if (answersText[index].text.ToString() == crntQ.answers[0]) StartCoroutine(trueOrFalse(true));
        else StartCoroutine(trueOrFalse(false));
    }
}
[System.Serializable]
public class QuestionList
{
    public string question;
    public string[] answers = new string[3];
}



