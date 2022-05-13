import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Card } from '../models/card.model';


@Injectable({
  providedIn: 'root'
})
export class CardsService {

  baseUrl = 'https://localhost:7286/api/Cards';

  constructor(private http: HttpClient) { }

  // Get all cards
  getAllCards() : Observable<Card[]>{
    return this.http.get<Card[]>(this.baseUrl);
  }

}
