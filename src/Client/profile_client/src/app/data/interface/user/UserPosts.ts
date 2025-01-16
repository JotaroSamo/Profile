import { BasePost } from "../post/BasePost";

export interface UserPosts {
    publicId: string;
    login: string;
    posts: BasePost[];
}
