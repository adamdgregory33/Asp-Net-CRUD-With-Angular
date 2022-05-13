import { Component, OnInit } from '@angular/core';
import { Card } from './models/card.model';
import { CardsService } from './service/cards.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'cards';
  cards: Card[] = [];
  card: Card = AppComponent.createCard();

  constructor(private cardsService: CardsService)
  {

  }
  ngOnInit(): void {
    this.getAllCards();
  }

  getAllCards(){
    this.cardsService.getAllCards().subscribe(
      response =>
      this.cards = response
    );
  }

  onSubmitCard(){
    // No card id means that we are creating a new card as opposed to an existing card
    if(this.card.id === ''){
      this.cardsService.addCard(this.card)
      .subscribe(
        response => {
          this.getAllCards();
          this.card = AppComponent.createCard();
        }
      );
    }
    else{
      this.cardsService.updateCard(this.card).subscribe(response =>{
          this.getAllCards();
      });
    }
  }

  onDeleteCard(id: string){
    this.cardsService.deleteCard(id)
    .subscribe(response =>{
      this.getAllCards();
    });
  }

  populateForm(card: Card){
    this.card = card;
  }

  static createCard(): Card{
    return {
      id:'',
      cardNumber:'',
      cardholderName:'',
      expiryMonth:'',
      expiryYear:'',
      secureCode:''
    }
  }
}
