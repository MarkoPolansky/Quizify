# IW5 projekt

Url Projektu :
https://app-iw5-2023-team-xletak00-web.azurewebsites.net
---

# Cíl
Cílem je vytvořit použitelnou a snadno rozšiřitelnou aplikaci, která splňuje požadavky zadání. Aplikace nemá padat nebo zamrzávat.

Zadání úmyslně není striktní, je Vám ponechána volnost, pro vlastní realizaci. Při hodnocení je kladen důraz na technické zpracování a kvalitu kódu, ale hodnotíme i použitelnost a grafické zpracování aplikace. Pokud Vám přijde, že v zadání chybí nějaká funkcionalita, neváhejte ji doplnit. Pište aplikaci tak, abyste ji sami chtěli používat.

---
## Data
V rámci dat, se kterými se bude pracovat budeme požadovat minimálně následující data.

### Otázka
- Text
- Odpovědi

### Odpověď
- Typ
- Text
- Obrázek
- Příznak správnosti

### Uživatel
- Jméno
- Fotografie (postačí url)

### Kvíz
- Otázky
- Datum a čas konání
- Vybrané odpovědi
- Skóre pro jednotlivé uživatele

---
## Funkcionalita
Webová aplikace bude obsahovat několik stránek pro zobrazování a zadávání dat.

V zadání není požadováno perzistentní uložení dat. To znamená, že když se aplikace restartuje, tak může o data přijít. Nicméně bude nutno data ukládat za běhu aplikace, aby bylo možno demonstrovat, že když se například pomocí aplikace přidá nový záznam, tak se tento zobrazí v příslušném seznamu záznamů, dá se editovat, smazat atd.

Minimální rozsah, který je požadován v rámci projektu je popsán v této kapitole.

## Stránka typu "seznam" pro každou datovou entitu
Seznam bude obsahovat všechny záznamy daného typu dostupné v aplikaci. Bude možno se z něj překliknout na detail záznamu a na pohled pro přidání nového záznamu.

## Stránka typu "detail" pro každou datovou entitu
Zobrazuje detail daného typu záznamu se všemi informacemi o něm. Editace záznamu může být implementována na stránce "detail", nebo na samostatné stránce.

### Stránka "Vyhledávání"
Stránka, na které můžete použít textové vyhledávání napříč záznamy v aplikaci. Seznam všech nalezených záznamů se zobrazí na stránce a bude se dát překlikem dostat na detail daného záznamu (tedy například v případě týmu se odnaviguje na detail týmu). Textově se vyhledává minimálně v těchto atributech:
- Otázka
    - Text
- Odpověď
    - Text
- Uživatel
    - Jméno

---
## Správa projektu - Azure DevOps
Projekt řeší studenti v týmech. V každém týmu jsou **3 studenti**.

Při řešení projektu týmy využívají Azure DevOps a využívají GIT na sdílení kódu.
## Architektura projektu

Ve výuce Vám ukazujeme nějakou strukturu organizace kódu do logických vrstev a projektů se zapojením návrhových vzorů. Pokoušíme se vysvětlit proč je vzorový projekt takhle organizovaný a proč jsou zvoleny jednotlivá rozhodnutí.

Budeme tedy i po Vás chtít logické rozvržení projektu. Můžete využít to, jak je organizovaný vzorový projekt probíraný na cvičeních a inspirovat se tímto uspořádáním (můžete ho mít stejné, za to Vám rozhodně body nestrhnem). Nebo můžete využít i vlastní uspořádání - v tom případě ale po Vás budeme chtít vysvětlit proč jste němu přistoupili a čím se jeho jednotlivé aspekty řídí.

V každém případě ale budeme chtít, aby výsledné řešení obsahovalo víc projektů a vrstev. Snažíme se Vám na tomto projektu ukázat nějakou základní architekturu SW projektu, abyste si odnesli i něco víc než jen to, že budete znát syntax jazyka C#. Na tenhle aspekt tedy rozhodně bude brán zřetel ve všech fázích hodnocení projektu.

## Nasazení do Azure

V rámci přednášek se budeme věnovat také nasazení celého řešení do prostředí Azure. Zkusíte si tedy nasadit všechny části Vašeho řešení a také automatizaci nasazování celého systému. Při pojmenování webů, databáze (pokud ji budete používat) a dalších částí, které budete vytvářet vycházejte z návodu, který máte k dispozici v rámci 1. přednášky. Také nezapomeňte přiřadit přístup k projektové části Azure pro učitelský účet (dle pokynů v 1. přednášce).

Schéma pojmenování věcí, které budete potřebovat založit v Azure je ukázána v prezentaci k 1. přednášce - prosím držte se tohoto schématu.

# Odevzdávání
Odevzdávání projektu má **2 fáze**. V každé fázi se hodnotí jiné vlastnosti projektu. Nicméně fáze na sebe navzájem následují a studenti pokračují v práci na svém kódu i po jeho odevzdání v rámci následující fáze.

---
### Fáze 1 – API (50 bodů)
V první fázi se zaměříme na vytvoření Web API služby. Výstupem tedy bude spustitelný projekt, který obsahuje Web API, poskytuje specifikaci ve standardu OpenAPI (výběr verze necháme na vás) a poskytuje přístup k API pomocí Swagger inspektoru. API obsahuje minimálně metody pro:
- Získání dat pro stránku typu "seznam" pro každou datovou entitu
- Získáni dat pro stránku typu "detail" pro každou datovou entitu
- Vytvoření záznamu pro každou datovou entitu
- Upravení existujícího záznamu pro každou datovou entitu
- Smazání záznamu pro každou datovou entitu
- Získání výsledků vyhledávání

Vzorové API, dle kterého se můžete inspirovat bude ukazováno na přednáškách/cvičeních.

V 1. fázi bude také požadováno pokrytí API testy. Minimálně musí být pokryty všechny API endpointy dostatečným počtem testů, aby se pomocí nich dala ověřit správnost funkcionality API.

Počítáme tedy s tím, že budete mít vytvořeny testy, které můžeme spustit jak lokálně, tak v rámci Azure DevOps a tyto testy testují správnost Vašeho řešení. To, jak psát testy bude ukázáno v rámci přednášek/cvičení.

Budeme tedy kontrolovat jak to, že máte napsány správné testy tak to, že aplikace funguje.

Hodnotíme:
- logický návrh tříd
- splnění funkcionality
- využití abstrakce, zapouzdření, polymorfismu
- čistotu kódu
- verzování v GITu po logických částech
- testy
- automatizované nasazení do Azure (CI + CD) z Azure DevOps
- logické rozšíření datového návrhu nad rámec zadání (bonusové body)

---
### Fáze 2 - Web (50 bodů)
V druhé fázi se od vás bude požadovat vytvoření webové aplikace pomocí technologie Blazor WebAssembly. Webová aplikace bude napojena na API vytvořeno v první fázi projektu.

Hodnotíme:
- opravení chyb a zapracování připomínek, které jsme vám dali v rámci hodnocení fáze 1
- funkčnost celé výsledné aplikace
- zobrazení jednotlivých informací dle zadání – seznam, detail, vytváření, editace, mazání…
- čistotu kódu
- vytvoření dobře vypadající aplikace (bonusové body)

---
## Obhajoba
Obhajoby projektů budou probíhat v **posledním týdnu** semestru. Termíny obhajob budou vyhlášeny v průběhu semestru.

Na obhajobu se dostaví **celý tým**. Z členů týmu bude cvičícími vybrán 1 student, který obhajobu povede. Na obhajobu **není nutné** mít prezentaci (Powerpoint nebo PDF). Budete nám muset ukázat, jak funguje váš kód, že je správně navržen. Obhajoby budou probíhat osobně, nebo online dle aktuálních omezení v době obhajob.

Připravte se na naše otázky k funkcionalitě jednotlivých tříd a k důvodům jejich členění.
