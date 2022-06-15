# Proiect-MDS
# Documentatia completa este disponibila in MAIN

# Source control

Pentru a putea lucra eficient, am folosit Github pentru a ne sincroniza munca si a o putea separa pe branch-uri, dar si pentru a putea raporta bug-uri. Fiind impartiti pe doua grupe, una lucrand la backend si una la frontend, am creat doua repository-uri, unul pentru fiecare grupa.

### Repo-ul backend

![Commit-uri backend](https://i.imgur.com/uDJOCRO.png)

Dupa cum se poate vedea, acest repo are 46 de commit-uri inainte de finalizarea proiectului si 15 merge-uri in branch-ul main.

De asemenea, au fost cateva bug-uri pe care nu le-am putut rezolva imediat, astfel ca am lansat un issue, iar cu ajutorul celorlalti, acestea au fost rezolvate.

![Bug-uri backend](https://i.imgur.com/BWgLDvH.png)

Cateva din branch-urile folosite sunt urmatoarele:

![Branch-uri](https://i.imgur.com/SLosCqM.png)

### Repo-ul frontend

![Commit-uri 1 frontend](https://i.imgur.com/v9OcOYv.png)

![Commit-uri 2 frontend](https://i.imgur.com/GwB7sjQ.png)

![Branch-uri backend](https://i.imgur.com/RReilhu.png)

Acest repo are de asemenea mai multe branch-uri cu un numar mare de commit-uri, dar si bug-uri care au fost eventual rezolvate.

![Bug-uri frontend](https://i.imgur.com/4ROzqhh.png)

#  Design-ul bazei de date

![Design](https://i.imgur.com/fJb5utC.png)

# Integration testing

In cazul programului nostru, unit testing-ul nu este foarte relevant, intrucat componentele functioneaza impreuna. De exemplu, toate functiile au nevoie de tabelele din baza de date pentru a functiona, iar baza de date nu poate fi testata singura. Astfel, folosim integration testing, acesta evaluand componentele pe un nivel mai larg, verificand daca componentele functioneaza impreuna. Fiind destul de asemanatoare, daca o functie functioneaza, suntem aproape siguri ca si restul functiilor de acelasi tip functioneaza. Testam, deci, cate o functie de fiecare tip: una pentru POST, una pentru GET, una pentru PUT, una pentru DELETE si o functie POST cu link pentru web scraping.

![Integration Testing](https://i.imgur.com/fF8PABp.png)

# User stories
![User Stories](https://i.imgur.com/Mt774yx.png)

# Backlog
![Backlog](https://i.imgur.com/5lrBP8R.png)