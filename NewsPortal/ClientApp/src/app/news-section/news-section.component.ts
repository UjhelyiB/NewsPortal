import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { News } from '../../services/types';
import { NewsModel } from '../../../../Models/TypescriptModels/NewsModel';
import { NewsService } from '../../services/news-service';
import { AuthGuard } from '../../../src/services/auth-guard.service';

@Component({
    selector: 'app-news-section',
    templateUrl: './news-section.component.html',
    styleUrls: ['./news-section.component.css']
})
export class NewsSectionComponent {

    @Input() post: NewsModel;
    @Output() sideNavbarNeedsToBeReduced = new EventEmitter<boolean>();

    constructor(public router: Router, private newsService: NewsService, private auth: AuthGuard) { }

    ngOnInit() {}

    goToPost(id: number) {
        this.router.navigate(['/post', id]);
    }

    deleteNews(id: number): void {
        this.newsService.deleteNews(id).subscribe(_ => {
            this.sideNavbarNeedsToBeReduced.emit(true);
        });
    }

    isLoggedIn() {
        return this.auth.getIsLoggedIn();
    }

}
