import { Component, OnInit } from '@angular/core';
import { NewsService } from "../../services/news-service"
import { News } from '../../services/types';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-news-post',
    templateUrl: './news-post.component.html',
    styleUrls: ['./news-post.component.css']
})
export class NewsPostComponent implements OnInit {

    post;
    id: number;

    constructor(private newsService: NewsService, private activateRoute: ActivatedRoute) { }

    ngOnInit() {
        this.activateRoute.params.subscribe(params => {
            if (params['id']) {
                this.id = params['id'];
                console.log(this.id);
                //this.getPost(this.id);
                /*this.newsService.getMockPost(this.id).subscribe(post => {
                    this.post = post;*/
            } else {
                console.log('Hiba');
            }
        });
        this.getPost(0);
    }

    getPost(id: number): void {
        // this.post = this.newsService.getMockPost(this.id);
        this.post = this.newsService.getMyMockedPost();
    }

    loadCategory(id: number): void {
        // TODO
    }

    share() {
        // TODO
    }
}
