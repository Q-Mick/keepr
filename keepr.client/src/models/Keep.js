import { Profile } from "./Account.js"

export class Keep{
constructor(data){
this.id = data.id
this.creatorId = data.creatorId
this.name = data.name
this.description = data.description
this.img = data.img || "https://st2.depositphotos.com/1252248/9434/v/600/depositphotos_94342060-stock-illustration-pop-art-comics-style.jpg";
this.views = data.views
this.kept = data.kept
this.creator = new Profile(data.creator)
this.vaultKeepId = data.vaultKeepId
}

}