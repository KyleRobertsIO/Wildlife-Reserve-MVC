/*
    Author: Kyle Roberts
*/

fetch('/api/creatures')
    .then(function (response) {
        return response.json();
    })
    .then(function (data) {
        let creatures = JSON.stringify(data);
        localStorage.setItem('creatures', creatures);
        createCreatureListings();
    });

function createCreatureListings() {
    let data = localStorage.getItem('creatures');
    document.getElementById("loading-screen-container").style.display = "none";
    if (data != null) {
        let creatures = JSON.parse(data);
        let template = document.getElementById("creature-listing-template").content;
        let gridContainer = document.getElementById("creature-info-list");
        creatures.forEach((creature) => {
            let clone = template.cloneNode(true);
            clone.querySelector('.nickname-title').innerText = creature.Nickname;
            clone.querySelector('.species-tag').innerText = creature.Species.Species;

            if (activeSession != "") {
                clone.querySelector(".delete-action").setAttribute("href",
                    `/home/creature_delete/${creature.CreatureID}`);
            }

            clone.querySelector(".view-action").setAttribute("href",
                `/home/creature/${creature.CreatureID}`);
            gridContainer.appendChild(clone);
        });
    }
}