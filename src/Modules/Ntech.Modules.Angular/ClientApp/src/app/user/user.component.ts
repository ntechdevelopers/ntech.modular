import { Component, OnInit } from '@angular/core';
import { UserService } from './user.service';
import { Author } from '../event/event.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})

export class UserComponent implements OnInit {

  listAuthor: Author[];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.getAuthors();
  }

  getAuthors(): void {
    this.userService.getAuthors().subscribe(n => this.listAuthor = n);
  }

}
