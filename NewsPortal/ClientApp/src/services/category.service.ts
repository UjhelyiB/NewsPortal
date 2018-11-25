import { Injectable, Inject } from "@angular/core";
import { Category } from "./types";
import { CATEGORIES } from "./mock-categories";
import { HttpClient } from "@angular/common/http";
import { CategoryModel } from "../../../Models/TypescriptModels/CategoryModel";
import { Observable } from "rxjs";

@Injectable()
export class CategoryService {

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

    getCategories(): Observable<CategoryModel[]> {
        return this.http.get<CategoryModel[]>(this.baseUrl + 'api/category/GetAllCategories');
    }

    updateCategory(id: number, content: CategoryModel): Observable<any> {
        return this.http.put(`${this.baseUrl}api/category/${id}`, content);
    }

    getMockCategories(): Category[] {
        return CATEGORIES;
    }
}
