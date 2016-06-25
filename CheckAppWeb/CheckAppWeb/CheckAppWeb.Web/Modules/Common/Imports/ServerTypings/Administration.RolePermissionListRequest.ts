namespace CheckAppWeb.Administration {
    export interface RolePermissionListRequest extends Serenity.ServiceRequest {
        RoleID?: number
        Module?: string
        Submodule?: string
    }
}

