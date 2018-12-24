import { Component, OnInit, Input } from '@angular/core';
import { Author } from '../event.component';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  @Input() author: Author;

  @Input() inputEvent: string;

  constructor() { }

  ngOnInit() {
  }

}
