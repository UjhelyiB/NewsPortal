import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { News } from '../../services/types';
import { NewsModel } from '../../../../Models/TypescriptModels/NewsModel';

@Component({
    selector: 'app-news-section',
    templateUrl: './news-section.component.html',
    styleUrls: ['./news-section.component.css']
})
export class NewsSectionComponent {

    @Input() post: NewsModel;

    constructor(public router: Router) { }

    ngOnInit() {}

    goToPost(id: number) {
        this.router.navigate(['/post', id]);
    }

}
