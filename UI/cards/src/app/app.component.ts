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
}
