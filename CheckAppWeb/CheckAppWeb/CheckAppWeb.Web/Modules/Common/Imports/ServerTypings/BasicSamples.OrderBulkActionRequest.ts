﻿namespace CheckAppWeb.BasicSamples {
    export interface OrderBulkActionRequest extends Serenity.ServiceRequest {
        OrderIDs?: number[]
    }
}

