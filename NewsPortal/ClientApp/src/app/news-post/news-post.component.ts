import { Component, OnInit } from '@angular/core';
import { NewsService } from "../../services/news-service"
import { News } from '../../services/types';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../services/category.service';
import { NewsModel } from '../../../../Models/TypescriptModels/NewsModel';

@Component({
    selector: 'app-news-post',
    templateUrl: './news-post.component.html',
    styleUrls: ['./news-post.component.css']
})
export class NewsPostComponent implements OnInit {

    post: NewsModel;
    id: number;

    constructor(private categoryService: CategoryService, private newsService: NewsService, private activateRoute: ActivatedRoute, private router: Router) { }

    ngOnInit() {
        

        this.activateRoute.params.subscribe(params => {
            if (params['id']) {
                this.id = params['id'];
                console.log(this.id);
                this.getPost(this.id);
                /*this.newsService.getMockPost(this.id).subscribe(post => {
                    this.post = post;*/
            } else {
                this.id = 0;
            }

            this.getPost(this.id);
        });
    }

    getPost(id: number): void {
        this.newsService.getNewsById(id).subscribe(res => {
            this.post = res;
            console.log(this.post);
        });
    }

    loadCategory(id: number): void {
        this.router.navigate(["category/" + id]);
    }

    getCategoryForId(categoryId: number) {
        return this.categoryService.getCategories().subscribe(res => {
            console.log(res);
            res.filter(_ => _.Id == categoryId)[0].Title;
        });
    }
}
