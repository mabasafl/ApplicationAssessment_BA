export interface AuthResult{
    success: boolean;
    message: string;
    token: string;
    refreshToken: string;
    tokenExpires: Date;

}