import { OnInit, Component, Input } from '@angular/core';
import { EventService } from './event.service';

@Component({
    selector: 'app-event',
    templateUrl: './event.component.html',
    styleUrls: ['./event.component.css']
})


export class EventComponent implements OnInit {

    inputAuthor: Author = new Author();

    event = {
        id: 1,
        eventName: `The Ntech's event`
    };

    selectedAuthor: Author;

    authors: Author[] = [
        { id: 11, name: 'Mr. Nice' },
        { id: 12, name: 'Narco' },
        { id: 13, name: 'Bombasto' },
        { id: 14, name: 'Celeritas' },
        { id: 15, name: 'Magneta' },
        { id: 16, name: 'RubberMan' },
        { id: 17, name: 'Dynama' },
        { id: 18, name: 'Dr IQ' },
        { id: 19, name: 'Magma' },
        { id: 20, name: 'Tornado' }
      ];

    constructor(private eventService: EventService) {}

    ngOnInit() {
        this.getAuthors();
    }

    onClick(author: Author) {
        console.log(author.id);
        this.selectedAuthor = author;
    }

    onAdd(inputAuthor: Author) {
        this.eventService.setAuthors().subscribe(a => this.authors.push(inputAuthor));
    }

    getAuthors(): void {
        this.eventService.setAuthors().subscribe(a => this.authors.push(a));
    }

}

export class Author {
    id: number;
    name: string;
}
