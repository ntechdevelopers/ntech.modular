import { Injectable } from '@angular/core';
import { Observable, of } from '../../../node_modules/rxjs';
import { Author } from './event.component';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  myAuthor: Author = { id: 21, name: 'Nam Vu' };

  constructor() { }

  setAuthors(): Observable<Author> {
    return of(this.myAuthor);
  }
}
