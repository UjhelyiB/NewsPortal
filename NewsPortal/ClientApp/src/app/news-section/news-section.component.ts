import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { News } from '../../services/types';

@Component({
    selector: 'app-news-section',
    templateUrl: './news-section.component.html',
    styleUrls: ['./news-section.component.css']
})
export class NewsSectionComponent {

    @Input() post: News;

    constructor(public router: Router) { }

    ngOnInit() {}

    goToPost(id: number) {
        this.router.navigate(['/post', id]);
    }

}
