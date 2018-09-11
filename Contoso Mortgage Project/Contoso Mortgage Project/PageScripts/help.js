
function handleResponse(status, response) {
    debugger;
    alert(response);
}

function searchAPI(search_terms)
///passes a search string, returns all knowledge-base articles formatted as html
{
    var result_articles = "failed";

    var req = new XMLHttpRequest();
    req.open("POST", "http://team3webapi.azurewebsites.net/api/knowledge");
    req.setRequestHeader("Content-Type", "application/xml;charset=UTF-8");
    // req.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    req.send("mortgage");
    req.onreadystatechange = function () {
        //  alert(this.readyState);
        // alert(this.readyState);

        if (req.readyState == 4) {
            alert(this.statusText);
            result_articles = this.response;
            result_articles = result_articles.substring(1, result_articles.length - 1);
        }
        return result_articles;
    };
    //req.close();

    //POST: http://team3webapi.azurewebsites.net/api/knowledge (return all publish articles by rating)
}


function getKBArticles() {
    let search = document.getElementById("searchInput").value;
    let xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            let kbList;
            if (this.responseText === "No Record Found.") {
                kbList === this.responseText;
            } else {
                let container = document.getElementById("kbResultsTaget");
                while (container.firstChild) {
                    container.removeChild(container.firstChild);
                }
                kbList = JSON.parse(this.responseText);
                for (var i = 0; i < kbList.length; i++) {
                    let element = document.createElement("DIV");
                    element.classList.add("profile");
                    element.classList.add("col-md-10");
                    element.classList.add("mb-4");

                    let title = document.createElement("div");
                    title.classList.add("row");
                    title.classList.add("mortgage_name");
                    title.classList.add("mortgage_panel_ele");
                    title.innerHTML = kbList[i]["Title"];
                    //=============================================================
                    let row1 = document.createElement("div");
                    row1.classList.add("row");
                    row1.classList.add("account_row");

                    let contentLabel = document.createElement("div");
                    contentLabel.classList.add("col-md-4");
                    contentLabel.innerHTML = "Content:";
                    
                    row1.appendChild(contentLabel);
                    //=============================================================
                    let row2 = document.createElement("div");
                    row2.classList.add("row");
                    row2.classList.add("account_row");
                    row2.classList.add("");

                    let contentValue = document.createElement("div");
                    contentValue.classList.add("col-md-4");
                    contentValue.innerHTML = kbList[i]["Content"];

                    row2.appendChild(contentValue);

                    element.appendChild(title);
                    element.appendChild(row1);
                    element.appendChild(row2);
                    //element.onclick = showMortgage.bind(this, true, i, termYears, balance);

                    container.appendChild(element);
                }
            }
        } else if (this.readyState === 4 && this.status === 400) {
            document.getElementById("error").innerHTML = "Case Request Failed.";
        }
    };
    xmlHttp.open("GET", "http://team3webapi.azurewebsites.net/api/knowledge/" + search, true);
    xmlHttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlHttp.send();
}