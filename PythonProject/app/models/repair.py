from datetime import datetime
from typing import Optional

from pydantic import BaseModel, Field

from app.models.enums import RepairStatus


class RepairRequest(BaseModel):
    id: Optional[int] = None
    vin: str
    car_number: str
    description: str
    status: RepairStatus = RepairStatus.NEW
    created_at: datetime = Field(default_factory=datetime.utcnow)


class StatusUpdatePayload(BaseModel):
    status: RepairStatus