import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Author } from '../event/event.component';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getAuthors(): Observable<Author[]> {
    return this.http.get<Author[]>('https://localhost:44392/api/ApiBase');
  }
}
